using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [TokenAuthenticationFilter, HttpGet]
        public ActionResult Chen(float num)
        {
            //Console.WriteLine($"Controller: {RouteData.Values["controller"]} Action: {RouteData.Values["action"]}");
            //return Ok(Configuration.GetConnectionString("dbChen"));
            /*using(ChenDbContext db = new ChenDbContext())
            {
                var chen = db.Customer.ToList();
                return Ok(chen);
            }*/
            StringValues testStringValue = new String[] { "Chen", "Chen2" };
            var chen = Request.Query["a"];
            var result = chen;
            var header = Request.Headers.TryGetValue("chen", out Microsoft.Extensions.Primitives.StringValues value);

            Console.WriteLine("Value length is " + chen.Count);
            foreach (var val in chen)
            {
                Console.WriteLine("Valus is: " + val);
            }
            Console.WriteLine("Full Value is " + chen);
            Console.WriteLine($"Is GivenName Member: {HttpContext.User.HasClaim(x => x.Type == ClaimTypes.GivenName && x.Value.Equals(GivenName.Member))}");
            Console.WriteLine($"Is Authorization: {HttpContext.User.Identity.IsAuthenticated}");
            return Ok(new { num = num, value = chen.ToString(), testStringValue = testStringValue.ToString() });
            //return Ok(new { num = num, chen = chen, value = value, chenLength = value.ToArray().Length, valuenull = string.IsNullOrEmpty(value) });
        }

        [HttpGet("testroute/{num}")]
        public ActionResult TestRoute([FromRoute]float num)
        {
            string chen = null;
            Console.WriteLine(chen);
            return Ok(new { num, chen });

        }

        [HttpPost]
        public ActionResult<User> Chenz(User user)
        {
            return user;
        }

        [HttpGet("testquery")]
        public ActionResult TestQuery()
        {
            using (ChenDbContext db = new ChenDbContext())
            {
                var data = db.UserInfo.Where(x => x.Id == 1);
                data = data.Where(x => x.lastName == "Chen is number one");
                return Ok(data.ToList());
                //return Ok(data.Select(x => new { x.lastName }).AsNoTracking().ToList());
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

            var position = positionList.Where(x => x.Latitude.Equals("456"));

            foreach (var data in position)
            {
                data.Latitude = "5555";
            }

            return Ok(position); //this will return empty string
        }

        [HttpGet("testfilter2")]
        [TokenAuthenticationFilter(Roles = "")]
        public ActionResult TestFilter2()
        {
            List<List<string>> stringList = new List<List<string>>()
            {
                new List<string> { "Chen1" },
                new List<string> { "Chen2" }
            };

            foreach (var list in stringList)
            {
                list[0] = "Worameth Semapat";
            }

            return Ok(stringList); //this will return empty string
        }

        [HttpGet("testitems")]
        [TokenAuthenticationFilter]
        public ActionResult TestItems()
        {
            var position = HttpContext.Items["Position"] as Position; //Same instance
            position.Longitude = "5555555";
            return Ok(HttpContext.Items["Position"]);
        }
    }
}