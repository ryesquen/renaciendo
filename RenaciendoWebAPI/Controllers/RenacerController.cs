using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RenaciendoWebAPI.Datos;
using RenaciendoWebAPI.Models.Dtos;

namespace RenaciendoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenacerController : ControllerBase
    {
        private readonly ILogger<RenacerController> _logger;

        public RenacerController(ILogger<RenacerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RenacerDTO>> GetRenacer()
        {
            _logger.LogInformation("Vamos por todas");
            return Ok(RenacerStore.renacerList);
        }

        [HttpGet("{RenacerId:int}", Name = "GetRenacerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<RenacerDTO> GetRenacerById(int RenacerId)
        {
            if (RenacerId == 0) { return BadRequest(); }
            var renacer = RenacerStore.renacerList.FirstOrDefault(r => r.RenacerId == RenacerId);
            if (renacer == null) { return NotFound(); }
            return Ok(renacer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<RenacerDTO> CrearRenacer([FromBody] RenacerDTO renacerDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(renacerDTO); }
            var existe = RenacerStore.renacerList.FindAll(p => p.Nombre?.ToLower() == renacerDTO.Nombre?.ToLower());
            if (existe.Count > 0)
            {
                ModelState.AddModelError("ExisteRenacer", "Ya existe en la Base de datos");
                return BadRequest(ModelState);
            }
            if (renacerDTO is null)
            {
                return BadRequest(renacerDTO);
            }
            if (renacerDTO.RenacerId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            renacerDTO.RenacerId = RenacerStore.renacerList.OrderByDescending(r => r.RenacerId).First().RenacerId + 1;
            RenacerStore.renacerList.Add(renacerDTO);
            return CreatedAtRoute("GetRenacerById", new { RenacerId = renacerDTO.RenacerId }, renacerDTO);
        }

        [HttpDelete("{RenacerId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int RenacerId)
        {
            if (RenacerId == 0) { return BadRequest(); }
            var existe = RenacerStore.renacerList.FirstOrDefault(r => r.RenacerId == RenacerId);
            if (existe == null) { return NotFound(); }
            RenacerStore.renacerList.Remove(existe);
            return NoContent();
        }

        [HttpPut("{RenacerId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRenacer(int RenacerId, [FromBody] RenacerDTO renacerDTO)
        {
            if (renacerDTO is null || RenacerId != renacerDTO.RenacerId)
            {
                return BadRequest(renacerDTO);
            }
            var renacer = RenacerStore.renacerList.FirstOrDefault(r => r.RenacerId == RenacerId);
            if (renacer == null) { return NotFound(); }
            renacer.Nombre = renacerDTO.Nombre;
            renacer.Dimension = renacerDTO.Dimension;
            renacer.Razon = renacerDTO.Razon;

            return NoContent();

        }

        [HttpPatch("{RenacerId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PatchRenacer(int RenacerId, JsonPatchDocument<RenacerDTO> renacerDTO)
        {
            if (renacerDTO is null || RenacerId == 0)
            {
                return BadRequest(renacerDTO);
            }
            var renacer = RenacerStore.renacerList.FirstOrDefault(r => r.RenacerId == RenacerId);
            if (renacer == null) { return NotFound(); }

            renacerDTO.ApplyTo(renacer, ModelState);
            if (!ModelState.IsValid) { return BadRequest(renacerDTO); }

            return NoContent();

            /*
                       [
                          {
                            "path": "/NOMBREPROPIEDADACAMBIAR",
                            "op": "replace",
                            "value": "NUEVOVALORDELAPROPIEDAD"
                          }
                    ]
             */


        }
    }
}