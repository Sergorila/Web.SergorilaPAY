using Entities;
using DAL.Interfaces;
using DAL.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class CategoryDao : BaseDao, ICategoryDao
{
    public CategoryDao(NpgsqlContext dbContext) : base(dbContext)
    {
    }

    public async Task<Category> GetCategoryItemAsync(int id)
    {
        var category = await DbContext.Categories
                    .FirstOrDefaultAsync(i => i.Id == id);
                return category;
    }

    public async IAsyncEnumerable<Product> GetCategoryItemsAsync(int id, int offset, int limit)
    {
        var currentCategory = DbContext.Categories
            .FirstOrDefault(c => c.Id == id);
        var elems = currentCategory?.Elems
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

    public async Task AddCategoryAsync(Category category)
    {
        await DbContext.Categories.AddAsync(category);
        await DbContext.SaveChangesAsync();
    }

    public async Task AddProductCategoryAsync(int id, Product product)
    {
        var category = await GetCategoryItemAsync(id);
        var categoryProducts = category.Elems.ToList();
        categoryProducts.Add(product);
        category.Elems = categoryProducts;
        DbContext.Categories.Update(category);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> RemoveCategoryAsync(int id)
    {
        var category = await GetCategoryItemAsync(id);
        DbContext.Categories.Remove(category);
        var removedCount = await DbContext.SaveChangesAsync();
        return removedCount != 0;
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        DbContext.Categories.Update(category);
        await DbContext.SaveChangesAsync();
    }
}