using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using FlareApi.Config;
using FlareApi.Entities;
using FlareApi.Service.Driver;
using Microsoft.IdentityModel.Tokens;

namespace FlareApi.Service
{
    public class TokenService : ITokenService
    {
        private readonly Settings _settings;

        public TokenService(Settings settings)
        {
            _settings = settings;
        }
        
        
        public string ExpediteToken(User user, Guid tokenId)
        {
           
            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, user.Role.Name),
                new(JwtRegisteredClaimNames.Sub, user.Uen),
                new(JwtRegisteredClaimNames.Jti, tokenId.ToString()),
            };

            var identity = new ClaimsIdentity(claims, FlarePolicy.FlareIdentity);
            var handler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_settings.GetEncodeSecret()),
                    SecurityAlgorithms.HmacSha256Signature),
            };
            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
        
        public string GetUserNameFromToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_settings.GetEncodeSecret()),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal =
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                return string.Empty;
            }

            var claim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var name = claim?.Value ?? string.Empty;
            return name;
        }
    }
}