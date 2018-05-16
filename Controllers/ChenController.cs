using Microsoft.AspNetCore.Mvc;

namespace testAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChenController : ControllerBase
    {
        [HttpGet]
        public ActionResult Chen()
        {
            return Ok("Values");
        }
    }
}