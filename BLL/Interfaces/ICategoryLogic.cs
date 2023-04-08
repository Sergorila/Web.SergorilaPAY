﻿using Entities;

namespace BLL.Interfaces;

public interface ICategoryLogic
{
    Task<Category> GetCategoryItemAsync(int id);
    IAsyncEnumerable<Product> GetCategoryItemsAsync(int id, int offset, int limit);
    Task AddCategoryAsync(Category category);
    Task<bool> RemoveCategoryAsync(int id);
    
    Task AddProductCategoryAsync(int id, Product product);
    
    Task UpdateCategoryAsync(Category category);
}