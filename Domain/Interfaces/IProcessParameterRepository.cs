using Domain.Models;

namespace Domain.Interfaces;

public interface IProcessParameterRepository
{
    Task<List<ProcessParameters>> List();
    Task<ProcessParameters?> GetById(int id);
    Task<bool> Create(ProcessParameters processParameter);
    Task<bool> Update(ProcessParameters processParameter);
    Task<bool> Delete(ProcessParameters processParameter);
    Task<bool> ProcessParameterExist(int processId, int parameterId);
    
}