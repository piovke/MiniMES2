using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> List();
    Task<Product?> GetById(int id);
    Task<Product> Create(Product product);
    Task Update(Product product);
    Task<bool> Delete(int id);
}