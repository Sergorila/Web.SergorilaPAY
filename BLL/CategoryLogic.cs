using DAL.Interfaces;
using BLL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace BLL;

public class CategoryLogic : BaseLogic, ICategoryLogic
{
    private readonly ICategoryDao _dao;
    
    public CategoryLogic(ILogger<BaseLogic> logger, ICategoryDao dao) : base(logger)
    {
        _dao = dao;
    }

    public async Task<Category> GetCategoryItemAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying get category by ID: {Id}", id);
            
            var res = await _dao.GetCategoryItemAsync(id);
            
            Logger.LogInformation("Compete getting category by ID: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while getting category by ID: {Id}", id);
            throw;
        }
    }

    public IAsyncEnumerable<Product> GetCategoryItemsAsync(int id, int offset, int limit)
    {
        try
        {
            Logger.LogInformation("Trying get category items by ID: {Id}", id);
            
            var res = _dao.GetCategoryItemsAsync(id, offset, limit);
            
            Logger.LogInformation("Compete getting category items by ID: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while getting category by items ID: {Id}", id);
            throw;
        }
    }

    public async Task AddCategoryAsync(Category category)
    {
        try
        {
            Logger.LogInformation("Trying add category: {Id}", category.Id);
            await _dao.AddCategoryAsync(category);
            Logger.LogInformation("Complete adding category: {Id}", category.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while adding category: {Id}", category.Id);
            throw;
        }
    }

    public async Task<bool> RemoveCategoryAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying remove category by ID: {Id}", id);
            var isRemoved = await _dao.RemoveCategoryAsync(id);
            Logger.LogInformation("Complete removing category by ID: {Id}", id);
            return isRemoved;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while removing category by ID: {Id}", id);
            throw;
        }
    }
}