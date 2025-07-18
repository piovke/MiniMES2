using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MiniProductionDbContext _context;

    public ProductRepository(MiniProductionDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> List()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> Create(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Product?> GetById(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}