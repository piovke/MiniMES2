using Application.DTOs;
using Application.Services;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
namespace Presentation.Controllers

{
    [Route("Processes")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _service;

        public ProcessController(IProcessService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("AddProcess")]
        public async Task<IActionResult> AddProcess([FromBody] CreateProcessDto input)
        {
            var response = await _service.Create(input);
            if (response == "status should be OK or NOK")
            {
                return BadRequest("status should be OK or NOK");
            }
            if(response=="date should be from the future")
                return BadRequest("date should be from the future");

            if (response == "failed to add")
            {
                return BadRequest("failed to add");
            }
            if(response =="order doesnt exist")
            {
                return BadRequest("order doesnt exist");
            }

            return Ok("added");
        }

        [HttpGet]
        [Route("ShowProcesses")]
        public async Task<IActionResult> ShowProcesses()
        {
            var processes = await _service.List();
            if (!processes.Any())
            {
                return NoContent();
            }
            return Ok(processes);
        }

        [HttpDelete]
        [Route("DeleteProcess")]
        public async Task<IActionResult> DeleteProcess([FromQuery] int id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted)
            {
                return BadRequest();
            }

            return Ok("deleted");
        }

        [HttpPut]
        [Route("UpdateProcess")]
        public async Task<IActionResult> UpdateProcess([FromQuery] int id, [FromBody] CreateProcessDto input)
        {
           bool updated = await _service.Update(id, input);
           if (!updated)
           {
               return BadRequest();
           }
           return Ok("updated");
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var details = await _service.Details(id);
            if (details == null)
            {
                return BadRequest();
            }

            return Ok(details);
        }
    }
}