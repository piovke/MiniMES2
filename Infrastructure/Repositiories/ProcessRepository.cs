using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProcessRepository : IProcessRepository
{
    private readonly MiniProductionDbContext _context;
    
    public ProcessRepository(MiniProductionDbContext context)
    {
        _context = context;
    }

    public async Task<List<Process>> List()
    {
        return await _context.Processes
            .Include(o=>o.ProcessParameters)
            .ThenInclude(p=>p.Parameter)
            .Include(p=>p.Order)
            .ToListAsync();
    }

    public async Task<Process?> GetById(int id)
    {
        return await _context.Processes.FindAsync(id);
    }

    public async Task<bool> Create(Process process)
    {
        await _context.Processes.AddAsync(process);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(Process process)
    {
        _context.Processes.Update(process);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(Process process)
    {
        _context.Processes.Remove(process);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> OrderExist(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return false;
        }

        return true;
    }
}