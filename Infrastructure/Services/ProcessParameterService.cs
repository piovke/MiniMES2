using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;


namespace Infrastructure.Services;

public class ProcessParameterService : IProcessParametersService
{
    private readonly IProcessParameterRepository _repository;

    public ProcessParameterService(IProcessParameterRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<ProcessParameterDto>> List()
    {
        var processParameters = await _repository.List();
        var processParametersDto = processParameters
            .Select(p => new ProcessParameterDto
            {
                Value = p.Value,
                Parameter = new ParameterDto
                {
                    Id = p.Parameter.Id,
                    Name = p.Parameter.Name,
                    Unit = p.Parameter.Unit,
                },
                Process = new ProcessDto
                {
                    Id = p.Process.Id,
                    SerialNumber = p.Process.SerialNumber,
                    Status = p.Process.Status,
                }
            }).ToList();
        
        return processParametersDto;
    }

    public async Task<ProcessParameters?> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<ProcessParameterDto?> Details(int id)
    {
        var processParameter = await _repository.GetById(id);
        if (processParameter == null)
        {
            return null;
        }

        return new ProcessParameterDto
        {
            Value = processParameter.Value,
            Parameter = new ParameterDto
            {
                Id = processParameter.Parameter.Id,
                Name = processParameter.Parameter.Name,
                Unit = processParameter.Parameter.Unit,
            },
            Process = new ProcessDto
            {
                Id = processParameter.Process.Id,
                SerialNumber = processParameter.Process.SerialNumber,
                Status = processParameter.Process.Status,
            }
        };
    }

    public async Task<bool> Create(CreateProcessParameterDto ppDto)
    {
        //niepotrzebne bo parametry sie teraz dodaje przy dodawaniu proceu
        return false;
        // //validate
        // bool bothExist = await _repository.ProcessParameterExist(ppDto.ProcessId, ppDto.Parameter.);
        // if (!bothExist)
        // {
        //     return false;
        // }
        // //
        // ProcessParameters order = new ProcessParameters
        // {
        //     Value = ppDto.Value,
        //     ParameterId = ppDto.ParameterId,
        //     ProcessId = ppDto.ProcessId,
        // };
        // return await _repository.Create(order);
    }

    public async Task<bool> Update(int id, CreateProcessParameterDto ppDto)
    {
        return false;
        // var ppToUpdate =  await _repository.GetById(id);
        // if (ppToUpdate == null)
        // {
        //     return false;
        // }
        // //validate
        // bool bothExist = await _repository.ProcessParameterExist(ppDto.ProcessId, ppDto.ParameterId);
        // if (!bothExist)
        // {
        //     return false;
        // }
        // //
        // return await _repository.Update(ppToUpdate);
    }

    public async Task<bool> Delete(int id)
    {
        var  ppToDelete = await _repository.GetById(id);
        if (ppToDelete == null)
        {
            return false;
        }
        return await _repository.Delete(ppToDelete);
    }
}