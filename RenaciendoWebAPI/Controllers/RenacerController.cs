using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenaciendoWebAPI.Datos;
using RenaciendoWebAPI.Models;
using RenaciendoWebAPI.Models.Dtos;
using RenaciendoWebAPI.Repositories.IRepository;
using System.Net;

namespace RenaciendoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenacerController : ControllerBase
    {
        private readonly ILogger<RenacerController> _logger;
        private readonly IRenacerRepository _renacerRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public RenacerController(ILogger<RenacerController> logger, IRenacerRepository renacerRepository, IMapper mapper)
        {
            _logger = logger;
            _renacerRepository = renacerRepository;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetRenacer()
        {
            try
            {
                _logger.LogInformation("Vamos por todas!!!!!");
                var renaceres = await _renacerRepository.GetAllAsync();
                if (renaceres is not null)
                {
                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = _mapper.Map<IEnumerable<RenacerDTO>>(renaceres);
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.Message.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return _response;
        }

        [HttpGet("{RenacerId:int}", Name = "GetRenacerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetRenacerById(int RenacerId)
        {
            try
            {
                if (RenacerId == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var renacer = await _renacerRepository.GetAsync(r => r.RenacerId == RenacerId);
                if (renacer == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<RenacerDTO>(renacer);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessages = new List<string>() { ex.Message.ToString() };
                _response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RenacerDTO>> CrearRenacer([FromBody] RenacerDTO renacerDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(renacerDTO); }
            var existe = await _renacerRepository.GetAsync(p => p.Nombre!.ToLower() == renacerDTO.Nombre!.ToLower());
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

            await _renacerRepository.CreateAsync(
                nuevo
                );

            await _renacerRepository.SaveChangesAsync();

            return CreatedAtRoute("GetRenacerById", new { RenacerId = nuevo.RenacerId }, nuevo);
        }

        [HttpDelete("{RenacerId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int RenacerId)
        {
            if (RenacerId == 0) { return BadRequest(); }
            var existe = await _renacerRepository.GetAsync(r => r.RenacerId == RenacerId);
            if (existe == null) { return NotFound(); }
            await _renacerRepository.DeleteAsync(existe);
            await _renacerRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{RenacerId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRenacer(int RenacerId, [FromBody] RenacerDTO renacerDTO)
        {
            if (renacerDTO is null || RenacerId != renacerDTO.RenacerId)
            {
                return BadRequest(renacerDTO);
            }
            var renacer = _renacerRepository.GetAsync(r => r.RenacerId == RenacerId).Result;
            if (renacer is null) { return NotFound(); }

            renacer.Amenidad = renacerDTO.Amenidad;
            renacer.Dimension = renacerDTO.Dimension;
            renacer.ImageURL = renacerDTO.ImageURL;
            renacer.Detalle = renacerDTO.Detalle;
            renacer.Nombre = renacerDTO.Nombre;

            await _renacerRepository.UpdateAsync(renacer);

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