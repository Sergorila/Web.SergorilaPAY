using DAL.Interfaces;
using BLL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;
namespace BLL;

public class ProductLogic : BaseLogic, IProductLogic
{
    private readonly IProductDao _dao;
    
    public ProductLogic(ILogger<BaseLogic> logger, IProductDao dao) : base(logger)
    {
        _dao = dao;
    }

    public async Task AddProductAsync(Product product)
    {
        try
        {
            Logger.LogInformation("Trying add product: {Id}", product.Id);
            await _dao.AddProductAsync(product);
            Logger.LogInformation("Complete adding product: {Id}", product.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while adding product: {Id}", product.Id);
            throw;
        }
    }

    public async Task<Product> GetProductAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying get product by ID: {Id}", id);
            
            var res = await _dao.GetProductAsync(id);
            
            Logger.LogInformation("Compete getting product by ID: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while getting product by ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> RemoveProductAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying remove product by ID: {Id}", id);
            var isRemoved = await _dao.RemoveProductAsync(id);
            Logger.LogInformation("Complete removing product by ID: {Id}", id);
            return isRemoved;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while removing product by ID: {Id}", id);
            throw;
        }
    }

    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            Logger.LogInformation("Trying update product: {Id}", product.Id);
            
            await _dao.UpdateProductAsync(product);
            Logger.LogInformation("Complete updating product: {Id}", product.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while updating product: {Id}", product.Id);
            throw;
        }
    }
}