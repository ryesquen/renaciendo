using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RenaciendoWebAPI.Datos;
using RenaciendoWebAPI.Models;
using RenaciendoWebAPI.Models.Dtos;

namespace RenaciendoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenacerController : ControllerBase
    {
        private readonly ILogger<RenacerController> _logger;
        private readonly RenacerContext _renacerContext;

        public RenacerController(ILogger<RenacerController> logger, RenacerContext renacerContext)
        {
            _logger = logger;
            _renacerContext = renacerContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RenacerDTO>> GetRenacer()
        {
            _logger.LogInformation("Vamos por todas");
            return Ok(_renacerContext.Renaceres);
        }

        [HttpGet("{RenacerId:int}", Name = "GetRenacerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<RenacerDTO> GetRenacerById(int RenacerId)
        {
            if (RenacerId == 0) { return BadRequest(); }
            var renacer = _renacerContext.Renaceres.FirstOrDefault(r => r.RenacerId == RenacerId);
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
            var existe = _renacerContext.Renaceres.FirstOrDefault(p => p.Nombre!.ToLower() == renacerDTO.Nombre!.ToLower());
            if (existe is not null)
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

            var nuevo = new Renacer
            {
                Nombre = renacerDTO.Nombre,
                Amenidad = renacerDTO.Amenidad,
                Detalle = renacerDTO.Detalle,
                Dimension = renacerDTO.Dimension,
                ImageURL = renacerDTO.ImageURL,
                FechaCreacion = DateTime.Now
            };

            _renacerContext.Renaceres.Add(
                nuevo
                );

            _renacerContext.SaveChanges();

            return CreatedAtRoute("GetRenacerById", new { RenacerId = nuevo.RenacerId }, nuevo);
        }

        [HttpDelete("{RenacerId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int RenacerId)
        {
            if (RenacerId == 0) { return BadRequest(); }
            var existe = _renacerContext.Renaceres.FirstOrDefault(r => r.RenacerId == RenacerId);
            if (existe == null) { return NotFound(); }
            _renacerContext.Renaceres.Remove(existe);
            _renacerContext.SaveChanges();
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
            var renacer = _renacerContext.Renaceres.FirstOrDefault(r => r.RenacerId == RenacerId);
            if (renacer is null) { return NotFound(); }
            
            renacer.Amenidad = renacerDTO.Amenidad;
            renacer.Dimension = renacerDTO.Dimension;
            renacer.ImageURL = renacerDTO.ImageURL;
            renacer.Detalle = renacerDTO.Detalle;
            renacer.Nombre = renacerDTO.Nombre;
            renacer.FechaActualizacion = DateTime.Now;


            _renacerContext.Renaceres.Update(renacer);
            _renacerContext.SaveChanges();

            return NoContent();

        }

        //[HttpPatch("{RenacerId:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult PatchRenacer(int RenacerId, JsonPatchDocument<RenacerDTO> renacerDTO)
        //{
        //    if (renacerDTO is null || RenacerId == 0)
        //    {
        //        return BadRequest(renacerDTO);
        //    }
        //    var renacer = _renacerContext.Renaceres.FirstOrDefault(r => r.RenacerId == RenacerId);
        //    if (renacer == null) { return NotFound(); }

        //    renacerDTO.ApplyTo(renacer, ModelState);
        //    if (!ModelState.IsValid) { return BadRequest(renacerDTO); }

        //    return NoContent();

        //    /*
        //               [
        //                  {
        //                    "path": "/NOMBREPROPIEDADACAMBIAR",
        //                    "op": "replace",
        //                    "value": "NUEVOVALORDELAPROPIEDAD"
        //                  }
        //            ]
        //     */


        //}
    }
}