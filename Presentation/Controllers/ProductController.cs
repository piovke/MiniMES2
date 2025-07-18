using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Presentation.Controllers

{
    [Route("Products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto input)
        {
            if (await _service.Create(input))
            {
                return Ok("Success");
            };
            return BadRequest();
        }
        
        [HttpGet]
        [Route("ShowProducts")]
        public async Task<IActionResult> ShowProducts()
        {
            var products = await _service.List();

            if (products.Count == 0)
            {
                return NoContent();
            }
            return Ok(products);
        }
        
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var product = await _service.Details(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            var isDeleted = await _service.Delete(id);
            if (isDeleted)
            {
                return Ok("Success");
            }
            return BadRequest();
        }
        
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] CreateProductDto input, [FromQuery] int id)
        {
            var product = await _service.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            bool added = await _service.Update(id, input);
            if (added)
            {
                return Ok("Success");
            }
            return BadRequest("Failed to update product");
        }
    }
}