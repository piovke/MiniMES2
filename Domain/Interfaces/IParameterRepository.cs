using Domain.Models;

namespace Domain.Interfaces;

public interface IParameterRepository
{
    Task<List<Parameter>> List();
    Task<Parameter?> GetById(int id);
    Task<bool> Create(Parameter parameter);
    Task<bool> Update(Parameter parameter);
    Task<bool> Delete(Parameter parameter);
}