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
using SampleAPI.Data;
using SampleAPI.Options;
using SampleAPI.Services;

namespace SampleAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private JwtTokenManager _jwtManager;
 
        public AuthController(JwtSettings jwtSettings)
        {
            _jwtManager = new JwtTokenManager(jwtSettings);          
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
                            objResp= _jwtManager.GenerateToken(authLogin.Email, "Admin");
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