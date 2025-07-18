using Domain.Models;

namespace Domain.Interfaces;

public interface IMachineRepository
{
    Task<List<Machine>> List();
    Task<Machine?> GetById(int id);
    Task<Machine> Create(Machine machine);
    Task Update(Machine machine);
    Task<bool> Delete(int id);
}