using Entities;

namespace DAL.Interfaces;

public interface ICategoryDao
{
    Task<Category> GetCategoryItemAsync(int id);
    IAsyncEnumerable<Product> GetCategoryItemsAsync(int id, int offset, int limit);
    Task AddCategoryAsync(Category category);
    Task AddProductCategoryAsync(int id, Product product);
    Task<bool> RemoveCategoryAsync(int id);
    Task UpdateCategoryAsync(Category category);
}