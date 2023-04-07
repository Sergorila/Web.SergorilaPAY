using Entities;
namespace DAL.Interfaces;

public interface IProductDao
{
    Task AddProductAsync(Product product);
    Task<Product> GetProductAsync(int id);
    Task<bool> RemoveProductAsync(int id);
    Task UpdateProductAsync(Product product);
}