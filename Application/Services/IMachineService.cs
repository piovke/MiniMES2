using Application.DTOs;
using Domain.Models;

namespace Application.Services;

public interface IMachineService
{
    Task<List<MachineDto>> List();
    Task<Machine?> GetById(int id);
    
    //zwraca machine dto z orderami lub null
    Task<MachineDto?> Details(int id);
    Task<bool> Create(CreateMachineDto dto);
    Task<bool> Update(int id, CreateMachineDto dto);
    Task<bool> Delete(int id);
}