using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<OrderDto>> List()
    {
        var orders = await _repository.List();
        var ordersDto = orders
            .Select(o => new OrderDto
            {
                Id = o.Id,
                Code = o.Code,
                Machine = new MachineDto
                {
                    Id = o.Machine.Id,
                    Name = o.Machine.Name,
                },
                Product = new ProductDto
                {
                    Id = o.Product.Id,
                    Name = o.Product.Name,
                },
                Processes = o.Processes.Select(p => new ProcessDto
                {
                    Id = p.Id,
                    Status = p.Status
                }).ToList(),
                Quantity = o.Quantity,
            }).ToList();
        
        return ordersDto;
    }

    public async Task<Order?> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<OrderDto?> Details(int id)
    {
        var order = await _repository.GetById(id);
        if (order == null)
        {
            return null;
        }

        return new OrderDto
        {
            Id = order.Id,
            Code = order.Code,
            Machine = new MachineDto
            {
                Id = order.Machine.Id,
                Name = order.Machine.Name,
            },
            Product = new ProductDto
            {
                Id = order.Product.Id,
                Name = order.Product.Name,
            },
            Processes = order.Processes.Select(p => new ProcessDto
            {
                Id = p.Id,
                Status = p.Status
            }).ToList(),
            Quantity = order.Quantity,
        };
    }

    public async Task<bool> Create(CreateOrderDto orderDto)
    {
        //validate
        bool bothExist = await _repository.MachineProductExist(orderDto.MachineId, orderDto.ProductId);
        if (string.IsNullOrEmpty(orderDto.Code)|| !bothExist || orderDto.Quantity < 0)
        {
            return false;
        }
        //
        Order order = new Order
        {
            Code = orderDto.Code,
            MachineId = orderDto.MachineId,
            ProductId = orderDto.ProductId,
            Quantity = orderDto.Quantity,
        };
        return await _repository.Create(order);
    }

    public async Task<bool> Update(int id, CreateOrderDto order)
    {
        var orderToUpdate =  await _repository.GetById(id);
        if (orderToUpdate == null)
        {
            return false;
        }
        //validate
        bool bothExist = await _repository.MachineProductExist(order.MachineId, order.ProductId);
        if (string.IsNullOrEmpty(order.Code)|| !bothExist || order.Quantity < 0)
        {
            return false;
        }
        //
        orderToUpdate.Code = order.Code;
        orderToUpdate.MachineId = order.MachineId;
        orderToUpdate.ProductId = order.ProductId;
        orderToUpdate.Quantity = order.Quantity;
        return await _repository.Update(orderToUpdate);
    }

    public async Task<bool> Delete(int id)
    {
        var  orderToDelete = await _repository.GetById(id);
        if (orderToDelete == null)
        {
            return false;
        }
        return await _repository.Delete(orderToDelete);
    }
}