using Application.DTOs;
using Domain.Models;

namespace Application.Services;

public interface IOrderService
{
    Task<List<OrderDto>> List();
    Task<Order?> GetById(int id);
    Task<OrderDto?> Details(int id);
    Task<bool> Create(CreateOrderDto order);
    Task<bool> Update(int id, CreateOrderDto order);
    Task<bool> Delete(int id);
}