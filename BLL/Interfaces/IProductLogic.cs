using Entities;

namespace BLL.Interfaces;

public interface IProductLogic
{
    Task AddProductAsync(Product product);
    Task<Product> GetProductAsync(int id);
    Task<bool> RemoveProductAsync(int id);
    Task UpdateProductAsync(Product product);
}