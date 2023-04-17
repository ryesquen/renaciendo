using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MySQLDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var query = "Select id, name, description from thing";
            return Ok("hola");
        }
    }
}
