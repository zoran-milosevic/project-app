using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AuthService.Interface.Repository;

namespace AuthService.Data.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(List<Claim> userClaims)
        {
            // Debugging purposes only, set this to false for production
            //Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var currentTime = DateTime.UtcNow;

            var accessToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: userClaims,
                notBefore: currentTime,
                expires: currentTime.AddMinutes(Convert.ToDouble(_configuration["JWT:ExpireInMinutes"])),
                signingCredentials: creds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

            return token;
        }
    }
}