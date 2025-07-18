using Application.DTOs;
using Application.Services;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers

{
    [Route("Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("ShowOrders")]
        public async Task<IActionResult> ShowOrders()
        {
            var orders = await _service.List();
            if(orders.Count == 0) return NoContent();
            return Ok(orders);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto input)
        {
            var created = await _service.Create(input);
            if(created) return Ok("created");
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] CreateOrderDto input, [FromQuery] int id)
        {
            var  updated = await _service.Update(id, input);
            if(updated) return Ok("updated");
            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromQuery] int id)
        {
            var deleted = await _service.Delete(id);
            if(deleted) return Ok("deleted");
            return BadRequest();
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var order = await _service.Details(id);
            if(order == null) return NoContent();
            return Ok(order);
        }
    }
}