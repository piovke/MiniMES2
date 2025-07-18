using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class ParameterService: IParameterService
{
    private readonly IParameterRepository _repository;

    public ParameterService(IParameterRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<ParameterDto>> List()
    {
        var parameters = await _repository.List();
        var parameterDtos = parameters
            .Select(p=> new ParameterDto
            {
                Id = p.Id,
                Name = p.Name,
                Unit = p.Unit,
            }).ToList();
        return parameterDtos;
    }

    public async Task<Parameter?> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<bool> Create(CreateParameterDto dto)
    {
        //validate
        if (string.IsNullOrWhiteSpace(dto.Name)|| string.IsNullOrWhiteSpace(dto.Unit))
        {
            return false;
        }
        //
        var parameter = new Parameter
        {
            Name = dto.Name,
            Unit = dto.Unit,
        };
        bool success = await _repository.Create(parameter);
        if (success) return true;
        return false;
    }

    public async Task<bool> Update(int id, CreateParameterDto parameter)
    {
        //validate
        if (string.IsNullOrWhiteSpace(parameter.Name)|| string.IsNullOrWhiteSpace(parameter.Unit))
        {
            return false;
        }
        //
        var parameterToUpdate = await _repository.GetById(id);
        if (parameterToUpdate == null)
        {
            return false;
        }
        parameterToUpdate.Name = parameter.Name;
        parameterToUpdate.Unit = parameter.Unit;
        return await _repository.Update(parameterToUpdate);
    }

    public async Task<bool> Delete(int id)
    {
        var parameterToDelete = await _repository.GetById(id);
        if (parameterToDelete == null) return false;
        return await _repository.Delete(parameterToDelete);
    }
}