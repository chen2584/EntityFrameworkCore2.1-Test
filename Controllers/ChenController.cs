using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using testAPI.Models;

namespace testAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChenController : ControllerBase
    {
        [HttpGet]
        public ActionResult Chen(int num, int chen, [FromServices] IConfiguration Configuration, [FromServices] IHostingEnvironment env)
        {
            return Ok(Configuration.GetConnectionString("dbChen"));
            /*using(ChenDbContext db = new ChenDbContext())
            {
                var chen = db.Customer.ToList();
                return Ok(chen);
            }*/
            /*var chen = Request.Query["a"];
            var result = chen;
            var header = Request.Headers.TryGetValue("chen", out Microsoft.Extensions.Primitives.StringValues value);

            return Ok(new { num = num, chen = Convert.ToString(chen), value = Convert.ToString(value), chennull = string.IsNullOrEmpty(chen), valuenull = string.IsNullOrEmpty(value) });*/
        }

        [HttpPost]
        public ActionResult<User> Chenz(User user)
        {
            return user;
        }

        [HttpGet("testquery")]
        public ActionResult TestQuery()
        {
            using(ChenDbContext db = new ChenDbContext())
            {
                var data = db.UserInfo.Where(x => x.Id == 1);
                data = data.Where(x => x.lastName == "Chen is number one");
                data = data.Take(10);
                return Ok(data.Select(x => new {x.lastName }).AsNoTracking().ToList());
            }
        }

        [HttpGet("testnull")]
        public ActionResult<Position> TestNullVariable()
        {
            List<Position> positionList = new List<Position>();
            positionList.Add(new Position { Longitude = "1234", Latitude = "123456" });
            var position = positionList.Where(x => x.Latitude == "12345").FirstOrDefault();
            return position; // it will return 204 (No Content) even we force Ok(position)
        }

        [HttpPut("testnull")]
        public ActionResult TestPutReturn()
        {
            return Ok("Chemz");
        }

        [HttpGet("testfilter")]
        public ActionResult TestFilter()
        {
            List<Position> positionList = new List<Position>()
            {
                new Position { Longitude="1234", Latitude="456" },
                new Position { Longitude="456", Latitude="123" }
            };

            Position position = positionList.FirstOrDefault();
            position.Latitude = "567";
            position.Longitude = "8910";

            return Ok(positionList);
        }
    }
}