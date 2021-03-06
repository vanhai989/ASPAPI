﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using SigningCredentials = Microsoft.IdentityModel.Tokens.SigningCredentials;
using System.Security.Cryptography;
using CRUDApi.Models.AuthModels;

namespace CRUDApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RefeshTokensController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly AppSetting appSetting;
        public RefeshTokensController(AuthDbContext context, IOptions<AppSetting> option)
        {
            _context = context;
            appSetting = option.Value;
        }

        // GET: api/RefeshTokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefreshToken>>> GetRefeshTokens()
        {
           
            return await _context.RefeshTokens.ToListAsync();
        }
        // GET: api/RefeshTokens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefreshToken>> GetRefeshToken(string id)
        {
            var refeshToken = await _context.RefeshTokens.FindAsync(id);

            if (refeshToken == null)
            {
                return NotFound();
            }

            return refeshToken;
        }

        // POST: api/RefeshTokens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefreshToken>> PostRefeshToken(RefreshToken refeshToken)
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
        public async Task<IActionResult> Sigin([FromBody] InforUser inforUser)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(t => t.Usename.Equals(inforUser.Usename) && t.Password.Equals(inforUser.Password));
            if (user == null) return BadRequest();

            // create Token
            var jwtToken = GenerateToken(user);
            // create refesh token\
            var RefreshToken = GenerateRefreshToken(user);
            await _context.RefeshTokens.AddAsync(RefreshToken);
            await _context.SaveChangesAsync();

            return Ok(new SiginResultModel
            {
                RefeshToken = RefreshToken.Token,
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

            var domain = await _context.RefeshTokens.FirstOrDefaultAsync(t => t.Token.Equals(requestToken.Token) && t.CreateByIp.Equals(ipAddress));
            if (domain == null) return BadRequest();
            // revoke current refreshToken 
            domain.RevokedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            // generate new token and new refreshToken

            var jwtToken = GenerateToken(domain.User);
            var newRefreshToken = GenerateRefreshToken(domain.User);
            // no need to save token, but refreshToken needed
            await _context.RefeshTokens.AddAsync(newRefreshToken);
            await _context.SaveChangesAsync();
            return Ok(new SiginResultModel {
                 Token = jwtToken,
                 RefeshToken = newRefreshToken.Token
            });
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting.SecrectKey));
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Usename), // this value to display name token, should be username
                    new Claim(ClaimTypes.NameIdentifier, user.Id), // this value to distinct between evryUser token, should be userId
                    new Claim("Id", user.Id), // you can pass anything from client get
                    new Claim(ClaimTypes.Role, ""), // if want to role permission for user passing data in here!

                }),
                Expires = DateTime.UtcNow.AddMinutes(15), // Expires for token current is 15 minutes
                SigningCredentials = new SigningCredentials(tokenSecret, SecurityAlgorithms.HmacSha256),
            };
            try
            {
                tokenHandler.CreateToken(TokenDescriptor);
            }
            catch(Exception error)
            {
                Console.Write("CreateToken failed!" + error);
            }
            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token); // this is token string
        }

        private RefreshToken GenerateRefreshToken(User user)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            RefreshToken Result = new RefreshToken();
            try
            {
                Result = new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpireDate = DateTime.UtcNow.AddHours(12),
                    CreateDate = DateTime.UtcNow,
                    CreateByIp = GetIpAddress(),
                    UserId = user.Id
                };
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }


            return Result;
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

        private bool RefeshTokenExists(string id)
        {
            return _context.RefeshTokens.Any(e => e.Id == id);
        }
    }
}
