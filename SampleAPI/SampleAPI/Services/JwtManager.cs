using Microsoft.IdentityModel.Tokens;
using SampleAPI.Contracts.v1.Responses;
using SampleAPI.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleAPI.Services
{
    public class JwtTokenManager
    {
        private JwtSettings _jwtSettings;
        public JwtTokenManager(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        public AuthReponse GenerateToken(string userrId,string userRole)
        {
            var jwtTokenHndlr = new JwtSecurityTokenHandler();

            var secretKey = System.Text.Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                                    new Claim(JwtRegisteredClaimNames.Sub, userrId),
                                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                                    new Claim(JwtRegisteredClaimNames.Email, userrId),
                                    new Claim(type:"role", value:userRole),
                                   // new Claim(type:"tags.view", value:"true")

                                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHndlr.CreateToken(tokenDesc);

            return new AuthReponse
            {
                Success = true,
                Token = jwtTokenHndlr.WriteToken(token)
            };
        }
    }
}
