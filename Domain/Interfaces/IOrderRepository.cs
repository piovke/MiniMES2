using Domain.Models;

namespace Domain.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> List();
    Task<Order?> GetById(int id);
    Task<bool> Create(Order order);
    Task<bool> Update(Order order);
    Task<bool> Delete(Order order);
    Task<bool> MachineProductExist(int machineId, int productId);
}