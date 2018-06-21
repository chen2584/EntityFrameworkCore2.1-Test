using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using testAPI.Models;

namespace testAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static List<Position> position = new List<Position>();

        // GET api/values
        [HttpGet]
        [TokenAuthenticationFilter]
        public ActionResult<List<Position>> Get()
        {
            Console.WriteLine("Name: " + User.Identity.Name);
            Console.WriteLine("Total Claim: " + User.Claims.Count());
            Console.WriteLine("GivenName: " + User.FindFirst(ClaimTypes.GivenName).Value);
            return position;
        }

        [HttpPost("testarray")]
        public ActionResult testStringArray([FromBody]string chen)
        {
                var chenz = chen.Length == 0 ? "Null" : chen.Length.ToString();
                return Ok(chenz);
        }

        [HttpGet("{Latitude}/{Longitude}")]
        public ActionResult Getz(string Latitude, string Longitude)
        {
            position.Add(new Position { Latitude = Latitude, Longitude = Longitude });
            return Ok(new { Latitude, Longitude });
        }

        public ActionResult<string> Getzz(string Latitude, string Longitude)
        {
            position.Add(new Position { Latitude = Latitude, Longitude = Longitude });
            return Ok(new { Latitude, Longitude });
        }

        // GET api/values/5
        [HttpGet("chenzz/{id}/{name}")]
        public ActionResult Get(int id, [FromQuery]string namez)
        {
            List<string> list = new List<string>();
            list.Add("1");
            list.Add("2");
            list.Add("3");
            list.Add("4");
            list.Add("5");
            list.Insert(0, "6");
            return Ok(list);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post()
        {
            //var chen = Request.Form.Files.FirstOrDefault() == null ? "Null" : "Mai now";
            var chen = Request.Form.Files.ToList();
            return Ok(chen);
        }

        // PUT api/values/5
        [HttpPut]
        public ActionResult Put(int i)
        {
            return Ok(new { firstName = "Worameth", last = "Lastname", num = i });
        }

        // DELETE api/values/5
        [HttpDelete]
        public ActionResult Delete()
        {
            return Ok("Hello world");
        }

        [HttpGet("createdb")]
        public async Task<ActionResult> createDb()
        {
            using (ChenDbContext db = new ChenDbContext())
            {
                await db.Database.EnsureCreatedAsync();

                List<UserInfo> userList = new List<UserInfo>();
                userList.Add(new UserInfo { userName = "worameth", lastName = "semapat", email = "worameth.semapat@gmail.com" });
                userList.Add(new UserInfo { userName = "worameth2", lastName = "semapat2", email = "worameth.semapat2@gmail.com" });
                userList.Add(new UserInfo { userName = "worameth3", lastName = "semapat3", email = "worameth.semapat3@gmail.com" });
                await db.UserInfo.AddRangeAsync(userList);
                await db.SaveChangesAsync();

                List<Product> productList = new List<Product>();
                productList.Add(new Product { productName = "Item1", remain = 10, price = 10000 });
                productList.Add(new Product { productName = "Item2", remain = 20, price = 20000 });
                productList.Add(new Product { productName = "Item3", remain = 30, price = 30000 });
                await db.Product.AddRangeAsync(productList);
                await db.SaveChangesAsync();

                List<Order> orderList = new List<Order>();
                orderList.Add(new Order { UserId = 1, ProductId = 1, value = 10 });
                orderList.Add(new Order { UserId = 2, ProductId = 2, value = 20 });
                orderList.Add(new Order { UserId = 1, ProductId = 3, value = 30 });
                await db.Order.AddRangeAsync(orderList);
                await db.SaveChangesAsync();
            }
            return Ok("create db successfully");
        }

        [HttpGet("dropdb")]
        public async Task<ActionResult> dropDb()
        {
            using (ChenDbContext db = new ChenDbContext())
            {
                await db.Database.EnsureDeletedAsync();
            }
            return Ok("drop db successfully");
        }

        [HttpGet("testdelete")]
        public async Task<ActionResult> testdelete()
        {
            using (ChenDbContext db = new ChenDbContext())
            {
                UserInfo result = await db.UserInfo.Include(x => x.Order).FirstOrDefaultAsync();
                db.UserInfo.Remove(result);
                int affected = await db.SaveChangesAsync();
                return Ok($"deleted! {affected} row");
            }
        }

        [HttpGet("testlazy")]
        public async Task<ActionResult> testLazy()
        {
            using (ChenDbContext db = new ChenDbContext())
            {
                List<UserInfo> user = await db.UserInfo.ToListAsync<UserInfo>();
                UserInfo userinfo = user[0];

                var order = userinfo.Order.Select(x => new { x.OrderId, x.UserId, x.ProductId });

                return Ok(order);
            }
        }

        [TokenAuthenticationFilter]
        [HttpGet("testeager")]
        public async Task<ActionResult> testEager()
        {
            using (ChenDbContext db = new ChenDbContext(isLazy: true))
            {
                var user = await db.UserInfo.Include(x => x.Order).Where(x => x.Id == 1).ToListAsync();

                return Ok(user);
            }
        }

        [HttpGet("getposition")]
        public Position getPosition()
        {
            var position = new Position { Latitude = "123", Longitude = "456" };
            position.CalculateLocation();
            return position;
        }

    }
}
