using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace testAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChenController : ControllerBase
    {
        [HttpGet]
        public ActionResult Chen(int num)
        {
            var chen = Request.Query["a"];
            var result = chen;
            var header = Request.Headers.TryGetValue("chen", out Microsoft.Extensions.Primitives.StringValues value);
            
            

            return Ok(new { num = num, chen = Convert.ToString(chen), value = Convert.ToString(value), chennull = string.IsNullOrEmpty(chen), valuenull = string.IsNullOrEmpty(value) });
        }
    }
}