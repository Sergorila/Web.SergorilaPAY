using Entities;
using DAL.Interfaces;
using DAL.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ProductDao : BaseDao, IProductDao
{
    protected ProductDao(NpgsqlContext dbContext) : base(dbContext)
    {
    }

    public async Task AddProductAsync(Product product)
    {
        await DbContext.Products.AddAsync(product);
        await DbContext.SaveChangesAsync();
    }

    public async Task<Product> GetProductAsync(int id)
    {
        var product = await DbContext.Products
            .FirstOrDefaultAsync(i => i.Id == id);
        return product;
    }

    public async Task<bool> RemoveProductAsync(int id)
    {
        var product = await GetProductAsync(id);
        DbContext.Products.Remove(product);
        var removedCount = await DbContext.SaveChangesAsync();
        return removedCount != 0;
    }

    public async Task UpdateProductAsync(Product product)
    {
        DbContext.Update(product);
        await DbContext.SaveChangesAsync();
    }
}