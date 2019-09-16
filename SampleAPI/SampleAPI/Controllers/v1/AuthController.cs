using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SampleAPI.Contracts.v1;
using SampleAPI.Contracts.v1.Requests;
using SampleAPI.Contracts.v1.Responses;
using SampleAPI.Options;

namespace SampleAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private JwtSettings _jwtSettings;
        public AuthController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        [HttpPost(APIRoutes.Auth.Login)]
        public IActionResult Login(AuthLoginRequest authLogin)
        {
            var resp = GetLoginResponse(authLogin);
            if (resp.Success)
                return Ok(resp);

            else
              return BadRequest(resp);
        }

        private AuthReponse GetLoginResponse(AuthLoginRequest authLogin)
        {
            var objResp = new AuthReponse();
            try
            {
                if (authLogin != null)
                {
                    if (authLogin.Email == "t@gmail.com")
                    {
                        if (authLogin.Password =="p123")
                        {
                            var jwtTokenHndlr = new JwtSecurityTokenHandler();

                            var secretKey = System.Text.Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                            var tokenDesc = new SecurityTokenDescriptor {
                                Subject = new System.Security.Claims.ClaimsIdentity(new [] 
                                {
                                    new Claim(JwtRegisteredClaimNames.Sub, authLogin.Email),
                                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                                    new Claim(JwtRegisteredClaimNames.Email, authLogin.Email)

                                }),
                                Expires=DateTime.UtcNow.AddHours(2),
                                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(secretKey),SecurityAlgorithms.HmacSha256Signature)
                            };
                            var token = jwtTokenHndlr.CreateToken(tokenDesc);

                            return new AuthReponse {
                                Success = true,
                                Token = jwtTokenHndlr.WriteToken(token)
                            };
                        }
                        else
                            objResp.Error = new ErrorResponse { Errors = new List<string> { "Invalid password" } };
                    }
                    else
                        objResp.Error = new ErrorResponse { Errors = new List<string> { "Invalid user" } };
                }
                else
                    objResp.Error = new ErrorResponse {Errors=new List<string> {"Invalid inputs" } };
            }
            catch (Exception ex)
            {
            }

            return objResp;

        }
    }
}