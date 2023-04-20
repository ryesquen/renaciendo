using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OracleWebAPI.Data.Models;
using OracleWebAPI.Services;
using System.Net;

namespace OracleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _categoriaService.GetAllCategories();
            if (response.Object is not null) return Ok(response);
            else return StatusCode(response.Status, response.Error);
        }

        [HttpGet("{id:decimal}")]
        public async Task<IActionResult> GetCategoryById(decimal id)
        {
            var response = await _categoriaService.GetCategoryById(id);
            if (response.Object is null)
            {
                switch (response.Status)
                {
                    case StatusCodes.Status404NotFound:
                        return NotFound(response);
                    case StatusCodes.Status400BadRequest:
                        return BadRequest(response);
                    case StatusCodes.Status500InternalServerError:
                        return StatusCode(response.Status, response.Error);
                }
            }
            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody] Categoria categoria)
        {
            if (categoria is null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var created = await _categoriaService.InsertCategory(categoria);
            return Created("", created);
        }

        [HttpPut("{id:decimal}")]
        public async Task<IActionResult> UpdateCategory(decimal id, [FromBody] Categoria categoria)
        {            
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var response = await _categoriaService.UpdateCategory(id, categoria);
            if (response.Exito) return Ok(response);
            else return StatusCode(response.Status, response.Error);
        }

        [HttpDelete("{id:decimal}")]
        public async Task<IActionResult> DeleteCategory(decimal id)
        {
            var response = await _categoriaService.DeleteCategory(id);
            if (response.Exito) return NoContent();
            else return StatusCode(response.Status, response.Error);
        }
    }
}
