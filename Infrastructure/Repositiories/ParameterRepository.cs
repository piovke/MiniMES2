using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ParameterRepository : IParameterRepository
{
    private readonly MiniProductionDbContext _context;

    public ParameterRepository(MiniProductionDbContext context)
    {
        _context = context;
    }

    public async Task<List<Parameter>> List()
    {
        return await _context.Parameters.ToListAsync();
    }

    public async Task<Parameter?> GetById(int id)
    {
        return await _context.Parameters.FindAsync(id);
    }
    
    public async Task<bool> Create(Parameter parameter)
    {
        await _context.Parameters.AddAsync(parameter);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> Update(Parameter parameter)
    {
        _context.Parameters.Update(parameter);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> Delete(Parameter parameter)
    {
        _context.Parameters.Remove(parameter);
        return await _context.SaveChangesAsync() > 0;
    }
}