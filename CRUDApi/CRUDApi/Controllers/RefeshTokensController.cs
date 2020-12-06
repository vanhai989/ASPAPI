﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Data;
using CRUDApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SigningCredentials = Microsoft.IdentityModel.Tokens.SigningCredentials;
using System.Security.Cryptography;
using Polly;
using Microsoft.Ajax.Utilities;

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefeshTokensController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly ConnectionStrings connectionStrings;

        public RefeshTokensController(AuthDbContext context, IOptions<ConnectionStrings> option)
        {
            _context = context;
            connectionStrings = option.Value;
        }

        // GET: api/RefeshTokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefeshToken>>> GetRefeshTokens()
        {
            return await _context.RefeshTokens.ToListAsync();
        }

        // GET: api/RefeshTokens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefeshToken>> GetRefeshToken(string id)
        {
            var refeshToken = await _context.RefeshTokens.FindAsync(id);

            if (refeshToken == null)
            {
                return NotFound();
            }

            return refeshToken;
        }

        // PUT: api/RefeshTokens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefeshToken(string id, RefeshToken refeshToken)
        {
            if (id != refeshToken.Id)
            {
                return BadRequest();
            }

            _context.Entry(refeshToken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefeshTokenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RefeshTokens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefeshToken>> PostRefeshToken(RefeshToken refeshToken)
        {
            _context.RefeshTokens.Add(refeshToken);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RefeshTokenExists(refeshToken.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRefeshToken", new { id = refeshToken.Id }, refeshToken);
        }

        // api to sigin and generate token
        [HttpPost("Sigin")]
        public async Task<IActionResult> Sigin([FromBody] User abtracToken)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(t => t.usname.Equals(abtracToken.usname) && t.password.Equals(abtracToken.password));
            if (user == null) return BadRequest();

            // create Token
            var jwtToken = GenerateToken(user);
            // create refesh token\
            var RefreshToken = GenerateRefreshToken(user);
            await _context.RefeshTokens.AddAsync(RefreshToken);
            return Ok(new SiginResultModel
            {
                RefeshToken = RefreshToken.token,
                Token = jwtToken
            });
        }

        // get request token from client
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RequestTokenModel requestToken)
        {
            // validate ModalState if it's inValid return badRequest
            if (!ModelState.IsValid) return BadRequest();
            // get ipAdress for request
            var ipAddress = GetIpAddress();

            var domain = await _context.RefeshTokens.FirstOrDefaultAsync(t => t.token.Equals(requestToken.Token) && t.CreateByIp.Equals(ipAddress));
            if (domain == null) return BadRequest();
            // revoke current refreshToken 
            domain.revokedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // generate new token and new refreshToken

            var jwtToken = GenerateToken(domain.User);
            var newRefreshToken = GenerateRefreshToken(domain.User);
            // no need to save token, but refreshToken needed
            await _context.RefeshTokens.AddAsync(newRefreshToken);
            await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return Ok(new SiginResultModel {
                 Token = jwtToken,
                 RefeshToken = newRefreshToken.token
            });
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSecret = Encoding.ASCII.GetBytes(connectionStrings.secrectKey);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.usname), // this value to display name token, should be username
                    new Claim(ClaimTypes.NameIdentifier, user.Id), // this value to distinct between evryUser token, should be userId
                    new Claim("Id", user.Id), // you can pass anything from client get
                    new Claim(ClaimTypes.Role, ""), // if want to role permision for user passing data in here!

                }),
                Expires = DateTime.UtcNow.AddMinutes(15), // Expires for token current is 15 minutes
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenSecret), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token); // this is token string

        }

        private RefeshToken GenerateRefreshToken(User user)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefeshToken
                {
                    token = Convert.ToBase64String(randomBytes),
                    expireDate = DateTime.UtcNow.AddHours(12),
                    createDate = DateTime.UtcNow,
                    CreateByIp = GetIpAddress(),
                    userId = user.Id
                };
            }
        }
        // utility function get ip address with request from client
    private string GetIpAddress()
    {
        string ip;
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            ip = Request.Headers["X-Forwarded-For"];
        else
            ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        if (string.IsNullOrEmpty(ip)) return "";
        else return ip.Split(":")[0];
    }


    // DELETE: api/RefeshTokens/5
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefeshToken(string id)
        {
            var refeshToken = await _context.RefeshTokens.FindAsync(id);
            if (refeshToken == null)
            {
                return NotFound();
            }

            _context.RefeshTokens.Remove(refeshToken);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefeshTokenExists(string id)
        {
            return _context.RefeshTokens.Any(e => e.Id == id);
        }
    }
}