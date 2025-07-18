using Domain.Interfaces;
using Application.DTOs;
using Application.Services;
using Domain.Models;

namespace Infrastructure.Services;

public class MachineService: IMachineService
{
    private readonly IMachineRepository _repository;

    public MachineService(IMachineRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MachineDto>> List()
    {
        var machines = await _repository.List();
        return machines.Select(m=> new MachineDto
        {
            Id = m.Id,
            Name = m.Name,
        }).ToList();
    }

    public async Task<Machine?> GetById(int id)
    {
        return await _repository.GetById(id);
    }

     public async Task<bool> Create(CreateMachineDto machineDto)
     {
         if (string.IsNullOrWhiteSpace(machineDto.Name) || string.IsNullOrWhiteSpace(machineDto.Description))
         {
             return false;
         }
         
         var machine = new Machine
         {
             Name = machineDto.Name,
             Description = machineDto.Description,
         };
         
         await _repository.Create(machine);
         return true;
     }

     public async Task<bool> Delete(int id)
     {
         bool isDeleted = await _repository.Delete(id);
         return isDeleted;
     }

     //zwraca machine dto z orderami lub null
     public async Task<MachineDto?> Details(int id)
     {
         var machine = await _repository.GetById(id);
         if (machine == null)
         {
             return null;
         }
         
         return new MachineDto
         {
             Id = machine.Id,
             Name = machine.Name,
             Description = machine.Description,
             Orders = machine.Orders.Select(m => new OrderDto
             {
                 Id = m.Id,
                 Code = m.Code
             }).ToList()
         };
     }

     public async Task<bool> Update(int id, CreateMachineDto machineDto)
     {
         var machine = await _repository.GetById(id);
         if (machine == null)
         {
             return false;
         }

         if (string.IsNullOrWhiteSpace(machineDto.Name) || string.IsNullOrWhiteSpace(machineDto.Description))
         {
             return false;
         }
         
         machine.Name = machineDto.Name;
         machine.Description = machineDto.Description;
         await _repository.Update(machine);
         return true;
     }
}