using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly MiniProductionDbContext _context;

    public OrderRepository(MiniProductionDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Order>> List()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order?> GetById(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<bool> Create(Order order)
    {
        await _context.Orders.AddAsync(order);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> Update(Order order)
    {
        _context.Orders.Update(order);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> Delete(Order order)
    {
        _context.Orders.Remove(order);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<bool> MachineProductExist(int machineId, int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        var machine = await _context.Machines.FindAsync(machineId);
        return product != null && machine != null;
    }
}