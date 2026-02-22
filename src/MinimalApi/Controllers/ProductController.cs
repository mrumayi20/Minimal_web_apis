using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly List<string> Products = new()
        {
            "Laptop", "Mouse", "Keyboard", "Monitor"
        };

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            if (id < 0 || id > Products.Count - 1)
            {
                return NotFound(new { message = "Product not found!" });
            }

            return Ok(Products[id]);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] string product)
        {
            if (product == null)
            {
                return BadRequest(new { message = "Product cannot be empty" });
            }

            Products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = Products.Count - 1 }, product);
        }
    }
}