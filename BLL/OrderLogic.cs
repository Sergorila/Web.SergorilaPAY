using DAL.Interfaces;
using BLL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace BLL;

public class OrderLogic : BaseLogic, IOrderLogic
{
    private readonly IOrderDao _dao;
    
    public OrderLogic(ILogger<BaseLogic> logger, IOrderDao dao) : base(logger)
    {
        _dao = dao;
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying get order by ID: {Id}", id);
            
            var res = await _dao.GetOrderAsync(id);
            
            Logger.LogInformation("Compete getting order by ID: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while getting order by ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> CheckOrderAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying check order by ID: {id}", id);
            var isExist = await _dao.CheckOrderAsync(id);
            Logger.LogInformation("Complete checking order by ID: {id}", id);
            return isExist;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while checking order by ID: {id}", id);
            throw;
        }
    }

    public async Task AddProductAsync(int id, int idProduct)
    {
        try
        {
            Logger.LogInformation("Trying add product to order: {Id}", id);
            await _dao.AddProductAsync(id, idProduct);
            Logger.LogInformation("Complete adding product to order: {Id}", id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while adding product to order: {Id}", id);
            throw;
        }
    }

    public async Task AddOrderAsync(Order order)
    {
        try
        {
            Logger.LogInformation("Trying add order: {Id}", order.Id);
            await _dao.AddOrderAsync(order);
            Logger.LogInformation("Complete adding order: {Id}", order.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while adding order: {Id}", order.Id);
            throw;
        }
    }

    public async Task<bool> RemoveOrderAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying remove order by ID: {Id}", id);
            var isRemoved = await _dao.RemoveOrderAsync(id);
            Logger.LogInformation("Complete removing order by ID: {Id}", id);
            return isRemoved;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while removing order by ID: {Id}", id);
            throw;
        }
    }

    public async Task UpdateOrderAsync(Order order)
    {
        try
        {
            Logger.LogInformation("Trying update order: {Id}", order.Id);
            
            await _dao.UpdateOrderAsync(order);
            Logger.LogInformation("Complete updating order: {Id}", order.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while updating order: {Id}", order.Id);
            throw;
        }
    }

    public IAsyncEnumerable<Product> GetOrderItemsAsync(int id, int offset, int limit)
    {
        try
        {
            Logger.LogInformation("Trying get order items by ID: {Id}", id);
            
            var res = _dao.GetOrderItemsAsync(id, offset, limit);
            
            Logger.LogInformation("Compete getting order items by ID: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while getting order by items ID: {Id}", id);
            throw;
        }
    }
}