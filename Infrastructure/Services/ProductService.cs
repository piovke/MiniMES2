using Domain.Interfaces;
using Application.DTOs;
using Application.Services;
using Domain.Models;

namespace Infrastructure.Services;

public class ProductService: IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> List()
    {
        var products = await _repository.List();
        return products.Select(m=> new ProductDto
        {
            Id = m.Id,
            Name = m.Name,
        }).ToList();
    }

    public async Task<Product?> GetById(int id)
    {
        return await _repository.GetById(id);
    }

     public async Task<bool> Create(CreateProductDto productDto)
     {
         if (string.IsNullOrWhiteSpace(productDto.Name) || string.IsNullOrWhiteSpace(productDto.Description))
         {
             return false;
         }
         
         var product = new Product
         {
             Name = productDto.Name,
             Description = productDto.Description,
         };
         
         await _repository.Create(product);
         return true;
     }

     public async Task<bool> Delete(int id)
     {
         bool isDeleted = await _repository.Delete(id);
         return isDeleted;
     }

     //zwraca product dto z orderami lub null
     public async Task<ProductDto?> Details(int id)
     {
         var product = await _repository.GetById(id);
         if (product == null)
         {
             return null;
         }
         
         return new ProductDto
         {
             Id = product.Id,
             Name = product.Name,
             Description = product.Description,
             Orders = product.Orders.Select(m => new OrderDto
             {
                 Id = m.Id,
                 Code = m.Code
             }).ToList()
         };
     }

     public async Task<bool> Update(int id, CreateProductDto productDto)
     {
         var product = await _repository.GetById(id);
         if (product == null)
         {
             return false;
         }

         if (string.IsNullOrWhiteSpace(productDto.Name) || string.IsNullOrWhiteSpace(productDto.Description))
         {
             return false;
         }
         
         product.Name = productDto.Name;
         product.Description = productDto.Description;
         await _repository.Update(product);
         return true;
     }
}