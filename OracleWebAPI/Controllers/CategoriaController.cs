using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OracleWebAPI.Data.Models;

namespace OracleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ModelContext _modelContext;

        public CategoriaController(ModelContext modelContext)
        {
            _modelContext = modelContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _modelContext.Categoria.ToListAsync());
        }

        [HttpGet("{id:decimal}")]
        public async Task<IActionResult> GetCategoryById(decimal id)
        {
            return Ok(await _modelContext.Categoria.FirstOrDefaultAsync(c => c.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody] Categoria categoria)
        {
            if (categoria is null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var created = await _modelContext.Categoria.AddAsync(categoria);
            await _modelContext.SaveChangesAsync();
            decimal _id = await _modelContext.Categoria.MaxAsync(c => c.Id);
            var newCategoria = await _modelContext.Categoria.FirstOrDefaultAsync(c => c.Id == _id);
            return Ok(newCategoria);
        }

        [HttpPut("{id:decimal}")]
        public async Task<IActionResult> UpdateCategory(decimal id, [FromBody] Categoria categoria)
        {
            if (categoria is null || id != categoria.Id) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            _modelContext.Update(categoria);
            await _modelContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:decimal}")]
        public async Task<IActionResult> DeleteCategory(decimal id)
        {
            var category = await _modelContext.Categoria.FirstOrDefaultAsync(c => c.Id == id);
            _modelContext.Remove(category);
            await _modelContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
