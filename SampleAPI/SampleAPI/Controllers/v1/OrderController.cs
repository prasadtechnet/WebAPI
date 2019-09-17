using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Contracts.v1;
using SampleAPI.Extensions;

namespace SampleAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        public OrderController()
        {

        }

       [HttpGet(APIRoutes.Order.GetAll)]
       [Authorize(Policy = "TagViewer")]
        public IActionResult GetAll()
        {


            return Ok("Get all");
        }
        [HttpGet(APIRoutes.Order.Get)]
        public IActionResult GetById(string Id)
        {

            var getUserRole = HttpContext.GetUserRole();

            if (getUserRole == "Admin")
            {
                return Ok("you have all permissions");
            }


            return Unauthorized("You dont have permission to access");
        }
    }
}