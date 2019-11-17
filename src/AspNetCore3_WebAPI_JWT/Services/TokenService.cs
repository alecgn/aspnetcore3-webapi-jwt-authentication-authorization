
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AspNetCore3_WebAPI_JWT.Models;
using Microsoft.Extensions.Configuration;

namespace AspNetCore3_WebAPI_JWT.Services 
{
    public class TokenService 
    {
        private IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        
        public string GenerateToken(User user)
        {
            var keyStr = _config.GetValue<string>("AppSettings:PrivateKey");
            var tokenExpirationInMinutes = _config.GetValue<int>("AppSettings:TokenExpirationInMinutes");

            if (string.IsNullOrWhiteSpace(keyStr))
                return null;

            var keyBytes = Encoding.UTF8.GetBytes(keyStr);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Username),
                     new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}