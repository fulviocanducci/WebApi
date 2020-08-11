using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Settings;

namespace WebApi.Services
{
    public sealed class TokenService : ITokenService
    {
        public TokenConfigurations TokenConfigurations { get; }
        public SigningConfigurations SigningConfigurations { get; }

        public TokenService(TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations)
        {
            TokenConfigurations = tokenConfigurations;
            SigningConfigurations = signingConfigurations;
        }
        public TokenResult GenerateTokenResult(User user)
        {
            DateTime created = DateTime.Now;
            DateTime expires = created.AddHours(TokenConfigurations.Hours);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = TokenConfigurations.Issuer[0],
                Audience = TokenConfigurations.Audience[0],
                SigningCredentials = SigningConfigurations.SigningCredentials,
                NotBefore = created,
                Expires = expires,
                Subject = new ClaimsIdentity(new Claim[]
                {
                  new Claim(ClaimTypes.Name, user.Email),
                  new Claim(ClaimTypes.Email, user.Email)
                   //new Claim(ClaimTypes.Role, user.Role.ToString())
                })
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenResult(tokenHandler.WriteToken(token), created, expires, true);
        }

        public JsonResult GenerateTokenJsonResult(User user)
            => new JsonResult(GenerateTokenResult(user));
    }
}
