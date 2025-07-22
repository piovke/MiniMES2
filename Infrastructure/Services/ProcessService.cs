using Application;
using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Application.Services;

namespace Infrastructure.Services;

public class ProcessService : IProcessService
{
    private readonly IProcessRepository _repository;

    public ProcessService(IProcessRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<ProcessDto>> List()
    {
        var processes = await _repository.List();
        var processDtos = processes
            .Select(o => new ProcessDto
            {
                Id = o.Id,
                DateTime = o.DateTime,
                SerialNumber = o.SerialNumber,
                Status = o.Status,
                Order = new OrderDto
                {
                    Id = o.Order.Id,
                    Code = o.Order.Code,
                },
                ProcessParameters = o.ProcessParameters.Select(p => new ProcessParameterDto
                {
                    Id = p.Id,
                    Value = p.Value,
                    Parameter = new ParameterDto
                    {
                        Id = p.Parameter.Id,
                        Name = p.Parameter.Name,
                        Unit = p.Parameter.Unit,
                    }
                }).ToList(),
            }).ToList();
        return processDtos;
    }

    public async Task<Process?> GetById(int id)
    {
        return await _repository.GetById(id);
    }
    
    public async Task<ProcessDto?> Details(int id)
    {
        var process = await _repository.GetById(id);
        if (process == null)
        {
            return null;
        }

        var processDto = new ProcessDto
        {
            DateTime = process.DateTime,
            Id = process.Id,
            SerialNumber = process.SerialNumber,
            Status = process.Status,
            Order = new OrderDto
            {
                Id = process.Order.Id,
                Code = process.Order.Code,
            },
            ProcessParameters = process.ProcessParameters.Select(p=> new ProcessParameterDto
            {
                Id = p.Id,
                Parameter = new ParameterDto
                {
                    Id = p.Parameter.Id,
                    Name = p.Parameter.Name,
                    Unit = p.Parameter.Unit,
                }
            }).ToList(),
        };
        
        return processDto;
    }

    public async Task<Result<Process?>> Create(CreateProcessDto processDto)
    {
        var result = new Result<Process?>(false,"",null);  
        
        if (!(processDto.Status == "OK" || processDto.Status == "NOK"))
        {
            result.Message = "Status should be OK or NOK";
            return result;
        }

        if (processDto.DateTime < DateTime.Now)
        {
            result.Message = "Time should be from the future";
            return result;
        }

        var orderExists = await _repository.OrderExist(processDto.OrderId);
        if (!orderExists)
        {
            result.Message = "order doesnt exist";
            return result;
        }
        

        var process = new Process
        {
            SerialNumber = processDto.SerialNumber,
            Status = processDto.Status,
            OrderId = processDto.OrderId,
            DateTime = processDto.DateTime,
            ProcessParameters = processDto.ProcessParameters.Select(p=>new ProcessParameters
            {
                Value = p.Value,
                ParameterId = p.ParameterId
            }).ToList()
        };
        
        var response = await _repository.Create(process);
        if (response)
        {
            result.Success = true;
            result.Message = "added";
            result.Data = process;
            return result;
        }
        result.Message = "failed";
        return result;
    }

    public async Task<bool> Update(int id, CreateProcessDto processDto)
    {
        Process? processToUpdate = await _repository.GetById(id);
        if (processToUpdate == null)
        {
            return false;
        }
        if (!(processDto.Status == "OK" || processDto.Status == "NOK"))
        {
            return false;
        }
        processToUpdate.Status = processDto.Status;
        processToUpdate.SerialNumber = processDto.SerialNumber;
        processToUpdate.Order.Id = processDto.OrderId;
        return await _repository.Update(processToUpdate);
    }

    public async Task<bool> Delete(int id)
    {
        var processToDelete =  await _repository.GetById(id);
        if (processToDelete == null)
        {
            return false;
        }
        return await _repository.Delete(processToDelete);
    }

}