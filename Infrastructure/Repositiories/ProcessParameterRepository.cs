using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProcessParameterRepository: IProcessParameterRepository
{
    private readonly MiniProductionDbContext _context;

    public ProcessParameterRepository(MiniProductionDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ProcessParameters>> List()
    {
        return await _context.ProcessParameters.ToListAsync();
    }

    public async Task<ProcessParameters?> GetById(int id)
    {
        return await _context.ProcessParameters.FindAsync(id);
    }

    public async Task<bool> Create(ProcessParameters processParameter)
    {
        await _context.ProcessParameters.AddAsync(processParameter);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> Update(ProcessParameters processParameter)
    {
        _context.ProcessParameters.Update(processParameter);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> Delete(ProcessParameters processParameter)
    {
        _context.ProcessParameters.Remove(processParameter);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> ProcessParameterExist(int processId, int parameterId)
    {
        var process = await _context.Processes.FindAsync(processId);
        var parameter = await _context.Parameters.FindAsync(parameterId);
        
        if ( process!= null && parameter != null)
        {
            return true;
        }

        return false;
    }
}