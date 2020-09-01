
using AspNetCore3_WebAPI_JWT.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNetCore3_WebAPI_JWT.Services
{
    public class TokenService 
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string GenerateToken(User user)
        {
            var securityKeyStr = _configuration.GetValue<string>("AppSettings:SecurityKey");

            if (string.IsNullOrWhiteSpace(securityKeyStr))
                throw new ConfigurationErrorsException("SecurityKey cannot be null, empty or whitespace, check configuration file.");

            var securityKeyBytes = Encoding.UTF8.GetBytes(securityKeyStr);
            var tokenExpirationInMinutes = _configuration.GetValue<int>("AppSettings:TokenExpirationInMinutes");
            tokenExpirationInMinutes = (tokenExpirationInMinutes <= 0 ? 15 : tokenExpirationInMinutes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Username),
                     new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
