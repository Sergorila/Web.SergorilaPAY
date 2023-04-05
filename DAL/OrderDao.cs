using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.DBContext;
using Entities;

namespace DAL;

public class OrderDao : BaseDao, IOrderDao
{
    protected OrderDao(NpgsqlContext dbContext) : base(dbContext)
    {
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        var order = await DbContext.Orders
            .FirstOrDefaultAsync(b => b.Id == id);
        
        return order;
    }

    public async Task<bool> CheckOrderAsync(int id)
    {
        var order = await DbContext.Orders
            .FirstOrDefaultAsync(u => 
                u.Id == id);
        
        return order != null;
    }

    public async Task AddProductAsync(int id, Product product)
    {
        var order = await GetOrderAsync(id);
        var orderProducts = order.Products.ToList();
        orderProducts.Add(product);
        order.Products = orderProducts;
        DbContext.Orders.Update(order);
        await DbContext.SaveChangesAsync();
    }

    public async Task AddOrderAsync(Order order)
    {
        await DbContext.AddAsync(order);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> RemoveOrderAsync(int id)
    {
        var order = await GetOrderAsync(id);
        DbContext.Orders.Remove(order);
        var removedCount = await DbContext.SaveChangesAsync();
        return removedCount != 0;
    }

    public async Task UpdateOrderAsync(Order order)
    {
        DbContext.Orders.Update(order);
        await DbContext.SaveChangesAsync();
    }
}