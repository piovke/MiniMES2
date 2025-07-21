using Domain.Models;

namespace Domain.Interfaces;

public interface IProcessRepository
{
    Task<List<Process>> List();
    Task<Process?> GetById(int id);
    Task<bool> Create(Process process);
    Task<bool> Update(Process process);
    Task<bool> Delete(Process process);
    Task<bool> OrderExist(int id);
}