using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Contracts.v1;

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
        public IActionResult GetAll()
        {


            return Ok();
        }
        [HttpGet(APIRoutes.Order.Get)]
        public IActionResult GetById(string Id)
        {

            return Ok("ok");
        }
    }
}