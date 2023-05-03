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

    public IEnumerable<Category> GetCategoriesAsync()
    {
        return DbContext.Categories.ToList();
    }

    public async IAsyncEnumerable<Product> GetCategoryItemsAsync(int id, int offset, int limit)
    {
        var currentCategory = await DbContext.Categories
            .Where(c => c.Id == id).Include(c => c.Elems).FirstOrDefaultAsync();
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

    public async Task AddProductAsync(int id, int idProduct)
    {
        var product = DbContext.Products
            .FirstOrDefault(p => p.Id == idProduct);
        product.CategoryId = id;
        DbContext.Products.Update(product);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> RemoveCategoryAsync(int id)
    {
        var category = await GetCategoryItemAsync(id);
        foreach (var product in DbContext.Products)
        {
            if (product.CategoryId == id)
            {
                product.CategoryId = null;
            }
        }
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