using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RenaciendoWebAPI.Models;
using RenaciendoWebAPI.Models.Dtos;

namespace RenaciendoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenacerController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<RenacerDTO> GetRenacer()
        {
            return new List<RenacerDTO>() {
                new RenacerDTO { Nombre = "Nacimiento", RenacerId = 1},
                new RenacerDTO { Nombre = "Desarrollo", RenacerId = 2},
                new RenacerDTO { Nombre = "Más Crecimiento", RenacerId = 3}
            };
        }
    }
}