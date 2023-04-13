using Microsoft.AspNetCore.Mvc;
using RenaciendoWebAPI.Datos;
using RenaciendoWebAPI.Models.Dtos;

namespace RenaciendoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenacerController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<RenacerDTO>> GetRenacer()
        {
            return Ok(RenacerStore.renacerList);
        }

        [HttpGet("RenacerId:int", Name = "GetRenacerById")]
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
    }
}