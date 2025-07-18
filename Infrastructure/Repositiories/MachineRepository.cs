using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MachineRepository : IMachineRepository
{
    private readonly MiniProductionDbContext _context;

    public MachineRepository(MiniProductionDbContext context)
    {
        _context = context;
    }

    public async Task<List<Machine>> List()
    {
        return await _context.Machines.ToListAsync();
    }

    public async Task<Machine> Create(Machine machine)
    {
        await _context.Machines.AddAsync(machine);
        await _context.SaveChangesAsync();
        return machine;
    }

    public async Task<bool> Delete(int id)
    {
        var machine = await _context.Machines.FindAsync(id);
        if (machine == null)
        {
            return false;
        }
        _context.Machines.Remove(machine);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Machine?> GetById(int id)
    {
        return await _context.Machines.FindAsync(id);
    }

    public async Task Update(Machine machine)
    {
        _context.Machines.Update(machine);
        await _context.SaveChangesAsync();
    }
}