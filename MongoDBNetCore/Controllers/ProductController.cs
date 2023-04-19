using Microsoft.AspNetCore.Mvc;
using MongoDBNetCore.Models;
using MongoDBNetCore.Repositories;

namespace MongoDBNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductCollection _productCollection;

        public ProductController(IProductCollection productCollection)
        {
            _productCollection = productCollection;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productCollection.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productCollection.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<IActionResult> InsertCar([FromBody] Product product)
        {
            await _productCollection.InsertProduct(product);
            return Created("created", product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, Product product)
        {
            var _product = await _productCollection.GetProductById(id);

            if (_product is null)
            {
                return NotFound();
            }

            product.Id = _product.Id;

            await _productCollection.UpdateProduct(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productCollection.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }

            await _productCollection.DeleteProduct(id);

            return NoContent();
        }
    }
}
