using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement.Common;
using System.Diagnostics;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        #region Get All Data Product
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? name, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var query = DataStore.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue) { 
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return Ok(query.ToList());
        }
        #endregion

        #region Get Data Product By Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.id == id);
            if(product == null)
            {
                return NotFound(new { Message = "Product Not Found" });
            }
            return Ok(product);
        }
        #endregion

        [HttpPost]
        #region Create Data Product
        public IActionResult Create([FromBody] Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.id = DataStore.Products.Any() ? DataStore.Products.Max(p => p.id) + 1 : 1;
            product.CreatedAt = DateTime.UtcNow;
            DataStore.Products.Add(product);

            _logger.LogInformation("Product Created : {Name}", product.Name);
            return CreatedAtAction(nameof(GetById), new { id = product.id }, product);
        }
        #endregion

        #region Update data Product
        [HttpPut("{id}")]
        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody] Products updateProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = DataStore.Products.FirstOrDefault(p => p.id == id);
            if(existing == null)
            {
                return NotFound(new { Message = "Data not found" });
            }

            existing.Name = updateProduct.Name;
            existing.Description = updateProduct.Description;
            existing.Price = updateProduct.Price;

            _logger.LogInformation("Success to update product : {Id}", id);
            return Ok(existing);
        }
        #endregion

        #region Delete Data Product
        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.id == id);
            if(product == null)
            {
                return NotFound(new { Message = "Data not found" });
            }

            DataStore.Products.Remove(product);
            _logger.LogInformation("Product with id {Id} succes to delete", id);
            return NoContent();
        }
        #endregion
    }
}
