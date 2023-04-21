using Entities;

namespace DAL.Interfaces;

public interface IOrderDao
{
    Task<Order> GetOrderAsync(int id);
    Task<bool> CheckOrderAsync(int id);
    Task AddProductAsync(int id, int idProduct);
    Task AddOrderAsync(Order order);
    Task<bool> RemoveOrderAsync(int id);
    Task UpdateOrderAsync(Order order);
    IAsyncEnumerable<Product> GetOrderItemsAsync(int id, int offset, int limit);
}