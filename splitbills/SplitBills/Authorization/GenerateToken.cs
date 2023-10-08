using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SplitBills.Authorization
{
    internal class GenerateToken
    {
        private readonly IConfiguration _configuration;
        private JwtSecurityToken _token;

        public GenerateToken(string emailId, IConfiguration configuration)
        {
            _configuration = configuration;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,emailId)
            };
            
            _token = new JwtSecurityToken(_configuration["Jwt:Issuer"], 
                _configuration["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:credentials);
        }

        public string GetToken()
        {
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }
    }
}
