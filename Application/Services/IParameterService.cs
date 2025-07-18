using Application.DTOs;
using Domain.Models;

namespace Application.Services;

public interface IParameterService
{
    Task<List<ParameterDto>> List();
    Task<Parameter?> GetById(int id);
    Task<bool> Create(CreateParameterDto parameterDto);
    Task<bool> Update(int id, CreateParameterDto parameterDto);
    Task<bool> Delete(int id);
}