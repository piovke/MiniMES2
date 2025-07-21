using Application.DTOs;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers

{
    [Microsoft.AspNetCore.Components.Route("ProcessParameter")]
    [ApiController]
    public class ProcessParameterController : ControllerBase
    {
        private readonly MiniProductionDbContext _context;

        public ProcessParameterController(MiniProductionDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AddProcessParameter")]
        public IActionResult AddProcessParameter([FromBody] CreateProcessParameterDto input)
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetProcessParameters")]
        public IActionResult GetProcessParameters()
        {
            var processParameters = _context.ProcessParameters
                .Include(p=>p.Process)
                .Include(p=>p.Parameter)
                .Select(p=>new ProcessParameterDto
                {
                    Id = p.Id,
                    Value = p.Value,
                    Parameter = new ParameterDto{
                        Id = p.Parameter.Id,
                       Name = p.Parameter.Name,
                       Unit = p.Parameter.Unit
                    },
                    Process = new ProcessDto()
                    {
                        Id = p.Process.Id,
                        SerialNumber = p.Process.SerialNumber
                    }
                }).ToList();
            return Ok(processParameters);
        }

        [HttpPut]
        [Route("UpdateProcessParameter")]
        public IActionResult UpdateProcessParameter([FromBody] CreateProcessParameterDto input, [FromQuery] int id)
        {
            return Ok("Updated");
        }

        [HttpDelete]
        [Route("DeleteProcessParameter")]
        public IActionResult DeleteProcessParameter([FromQuery] int id)
        {
            var processParameterToDelete = _context.ProcessParameters.Find(id);
            if (processParameterToDelete == null)
            {
                return NotFound();
            }
            _context.ProcessParameters.Remove(processParameterToDelete);
            _context.SaveChanges();
            return Ok("deletd");
        }
    }
        
}
