using CRUDApi.Data;
using CRUDApi.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using SigningCredentials = Microsoft.IdentityModel.Tokens.SigningCredentials;
using System.Security.Cryptography;

namespace CRUDApi.Services.Impl
{
    public class SiginServiceIpml // : ISiginService
    {
        private readonly AuthDbContext _context;

        public SiginServiceIpml(AuthDbContext authDbContext)
        {
            _context = authDbContext;
        }
       /* public async Task<SiginResultModel> Sigin([FromBody] InforUser inforUser)
        {
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
        }*/
    }
}
