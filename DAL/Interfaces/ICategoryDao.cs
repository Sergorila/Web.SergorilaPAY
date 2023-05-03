using Entities;

namespace DAL.Interfaces;

public interface ICategoryDao
{
    Task<Category> GetCategoryItemAsync(int id);
    IEnumerable<Category> GetCategoriesAsync();
    IAsyncEnumerable<Product> GetCategoryItemsAsync(int id, int offset, int limit);
    Task AddCategoryAsync(Category category);
    Task AddProductAsync(int id, int idProduct);
    Task<bool> RemoveCategoryAsync(int id);
    Task UpdateCategoryAsync(Category category);
}