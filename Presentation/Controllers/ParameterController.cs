using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers

{
    [Route("Parameters")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private readonly IParameterService _service;

        public ParameterController(IParameterService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("AddParameter")]
        public async Task<IActionResult> AddParameter([FromBody] CreateParameterDto input)
        {
           bool added = await _service.Create(input);
           if (added) return Ok();
           return BadRequest();
        }

        [HttpGet]
        [Route("ShowParameters")]
        public async Task< IActionResult> ShowParameters()
        {
            var parameters = await _service.List();
            if (parameters.Count == 0) return NoContent();
            return Ok(parameters);
        }

        [HttpPut]
        [Route("UpdateParameter")]
        public async Task< IActionResult> UpdateParameter([FromQuery] int id, [FromBody] CreateParameterDto input)
        {
            bool updated = await _service.Update(id, input);
            if (updated) return Ok();
            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteParameter")]
        public async Task< IActionResult> DeleteParameter([FromQuery] int id)
        {
            bool deleted = await _service.Delete(id);
            if (deleted) return Ok();
            return BadRequest();
        }
    }
}