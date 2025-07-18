using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Presentation.Controllers

{
    [Route("Machines")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _service;
        public MachineController(IMachineService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("AddMachine")]
        public async Task<IActionResult> AddMachine([FromBody] CreateMachineDto input)
        {
            if (await _service.Create(input))
            {
                return Ok("Success");
            };
            return BadRequest();
        }
        
        [HttpGet]
        [Route("ShowMachines")]
        public async Task<IActionResult> ShowMachines()
        {
            var machines = await _service.List();

            if (machines.Count == 0)
            {
                return NoContent();
            }
            return Ok(machines);
        }
        
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var machine = await _service.Details(id);
            if (machine == null)
            {
                return NotFound();
            }
            return Ok(machine);
        }
        
        
        [HttpDelete]
        [Route("DeleteMachine")]
        public async Task<IActionResult> DeleteMachine([FromQuery] int id)
        {
            var isDeleted = await _service.Delete(id);
            if (isDeleted)
            {
                return Ok("Success");
            }
            return BadRequest();
        }
        
        [HttpPut]
        [Route("UpdateMachine")]
        public async Task<IActionResult> UpdateMachine([FromBody] CreateMachineDto input, [FromQuery] int id)
        {
            var machine = await _service.GetById(id);
            if (machine == null)
            {
                return NotFound();
            }

            bool added = await _service.Update(id, input);
            if (added)
            {
                return Ok("Success");
            }
            return BadRequest("Failed to update machine");
        }
    }
}