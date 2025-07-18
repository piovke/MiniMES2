using Application.DTOs;
using Domain.Models;

namespace Application.Services;

public interface IProductService
{
    Task<List<ProductDto>> List();
    Task<Product?> GetById(int id);
    Task<ProductDto?> Details(int id);
    Task<bool> Create(CreateProductDto dto);
    Task<bool> Update(int id, CreateProductDto dto);
    Task<bool> Delete(int id);
}