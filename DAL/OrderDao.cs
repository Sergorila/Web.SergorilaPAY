using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.DBContext;
using Entities;

namespace DAL;

public class OrderDao : BaseDao, IOrderDao
{
    public OrderDao(NpgsqlContext dbContext) : base(dbContext)
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
            .FirstOrDefaultAsync(o => 
                o.Id == id);
        
        return order != null;
    }

    public async Task AddProductAsync(int id, int idProduct)
    {
        var product = DbContext.Products
            .FirstOrDefault(p => p.Id == idProduct);
        product.OrderId = id;
        DbContext.Products.Update(product);
        await DbContext.SaveChangesAsync();
    }

    public async Task AddOrderAsync(Order order)
    {
        var user = DbContext.Users
            .FirstOrDefault(u => u.Id == order.UserId);
        order.UserId = user.Id;
        order.User = user;
        await DbContext.Orders.AddAsync(order);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> RemoveOrderAsync(int id)
    {
        var order = await GetOrderAsync(id);
        foreach (var product in DbContext.Products)
        {
            if (product.OrderId == id)
            {
                product.OrderId = null;
            }
        }
        DbContext.Orders.Remove(order);
        var removedCount = await DbContext.SaveChangesAsync();
        return removedCount != 0;
    }

    public async Task UpdateOrderAsync(Order order)
    {
        DbContext.Orders.Update(order);
        await DbContext.SaveChangesAsync();
    }

    public async IAsyncEnumerable<Product> GetOrderItemsAsync(int id, int offset, int limit)
    {
        var currentOrder = await DbContext.Orders
                    .Where(c => c.Id == id).Include(c => c.Products).FirstOrDefaultAsync();
                var elems = currentOrder?.Products
                    .Skip(offset).Take(limit);
                
                if (elems == null)
                {
                    yield break;
                }
                
                foreach (var item in elems)
                {
                    yield return item;
                }
    }
}