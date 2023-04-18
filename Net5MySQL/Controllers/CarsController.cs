using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net5MySQL.Models;
using System.Threading.Tasks;

namespace Net5MySQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly carsdbContext _carsdbContext;

        public CarsController(carsdbContext carsdbContext)
        {
            _carsdbContext = carsdbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            return Ok(await _carsdbContext.Cars.ToListAsync());
        }
    }
}
