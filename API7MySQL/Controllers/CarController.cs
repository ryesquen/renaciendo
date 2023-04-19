using CLData.Repository;
using CLModel;
using Microsoft.AspNetCore.Mvc;

namespace API7MySQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            return Ok(await _carRepository.GetAllCars());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            return Ok(await _carRepository.GetCarById(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertCar([FromBody] Car car)
        {
            if (car is null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var created = await _carRepository.InsertCar(car);
            return Created("created", created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car car)
        {
            if (car is null || id != car.Id) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            await _carRepository.UpdateCar(car);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carRepository.DeleteCar(new Car { Id = id });
            return NoContent();
        }
    }
}
