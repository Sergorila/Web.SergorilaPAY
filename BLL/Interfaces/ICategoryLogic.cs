using Entities;

namespace BLL.Interfaces;

public interface ICategoryLogic
{
    Task<Category> GetCategoryItemAsync(int id);
    IAsyncEnumerable<Product> GetCategoryItemsAsync(int id, int offset, int limit);
    Task AddCategoryAsync(Category category);
    Task<bool> RemoveCategoryAsync(int id);
    Task UpdateCategoryAsync(Category category);
    Task AddProductAsync(int id, int idProduct);
    IEnumerable<Category> GetCategoriesAsync();
}