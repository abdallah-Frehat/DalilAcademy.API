using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DalilAcademy.Infra.Security
{
    public class JwtTokenService(IOptions<JwtSetting> options) : ITokenService
    {
        public (string token, DateTime ExpiresAt) CreateToken(User user)
        {

            var jwt = options.Value;
            var expiresAt = DateTime.UtcNow.AddHours(jwt.ExpiresInHours);
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = jwt.Issuer,
                Audience = jwt.Audience,
                Expires = expiresAt,
                Subject = new ClaimsIdentity(
             [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
                ]),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                   SecurityAlgorithms.HmacSha256Signature)
            };
            return (new JsonWebTokenHandler().CreateToken(descriptor), expiresAt);
        } 

    }
}
