
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AspNetCore3_WebAPI_JWT.Models;
using Microsoft.Extensions.Configuration;
using AspNetCore3_WebAPI_JWT.Enums;
using CryptHash.Net.Encoding;
using System.Security.Cryptography;
using CryptHash.Net.Util;

namespace AspNetCore3_WebAPI_JWT.Services 
{
    public class TokenService 
    {
        private IConfiguration Configuration;

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public string GenerateToken(User user)
        {
            byte[] keyBytes = null;
            var keyStr = Configuration.GetValue<string>("AppSettings:PrivateKey");

            if (string.IsNullOrWhiteSpace(keyStr))
                //return null;
                keyBytes = CommonMethods.Generate256BitKey();

            if (keyBytes == null)
            {
                var keyEncodingType = Configuration.GetValue<EncodingType>("AppSettings:PrivateKeyEncodingType");

                switch (keyEncodingType)
                {
                    case EncodingType.Hexadecimal:
                        keyBytes = Hexadecimal.ToByteArray(keyStr);
                        break;
                    case EncodingType.Base64:
                        keyBytes = Base64.ToByteArray(keyStr);
                        break;
                    default:
                        break;
                }
            }

            var keySizes = new KeySizes(128, 256, 64); // allowed key sizes for modern symmetric algorithms

            if ((keyBytes.Length * 8) >= keySizes.MinSize && (keyBytes.Length * 8) <= keySizes.MaxSize && ((keyBytes.Length * 8) % keySizes.SkipSize == 0))
            {
                var tokenExpirationInMinutes = Configuration.GetValue<int>("AppSettings:TokenExpirationInMinutes");
                tokenExpirationInMinutes = (tokenExpirationInMinutes <= 0 ? 15 : tokenExpirationInMinutes);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
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
            else
                return null;
        }
    }
}