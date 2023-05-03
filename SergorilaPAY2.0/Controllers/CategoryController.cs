using AutoMapper;
using BLL;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SergorilaPAY2._0.Views;

namespace SergorilaPAY2._0.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryLogic _categoryLogic;
    private readonly IMapper _mapper;
    
    public CategoryController(ICategoryLogic categoryLogic, IMapper mapper)
    {
        _categoryLogic = categoryLogic;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("api/getcategory")]
    public async Task<IActionResult> GetCategory(int id)
    {
        try
        {
            var category = await _categoryLogic.GetCategoryItemAsync(id);
            if (category != null)
            {
                var res = _mapper.Map<Category>(category);
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
        catch (Exception)
        {
            IActionResult badRequestObjectResult = BadRequest("Bad request.");
            return badRequestObjectResult;
        }
    }
    
    [HttpGet]
    [Route("api/getcategories")]
    public IActionResult GetCategories()
    {
        try
        {
            var categories = _categoryLogic.GetCategoriesAsync();
            if (categories != null)
            {
                return Ok(categories);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
        catch (Exception)
        {
            IActionResult badRequestObjectResult = BadRequest("Bad request.");
            return badRequestObjectResult;
        }
    }
    
    [HttpGet]
    [Route("api/getcategoryitems")]
    public async Task<IActionResult> GetCategoryItems(int id, int offset, int limit)
    {
        try
        {
            var category = _categoryLogic.GetCategoryItemsAsync(id, offset, limit);
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
        catch (Exception)
        {
            IActionResult badRequestObjectResult = BadRequest("Bad request.");
            return badRequestObjectResult;
        }
    }
    
    [HttpPost]
    [Route("api/addcategory")]
    public async Task<IActionResult> AddGategory(CategoryView category)
    {
        try
        {
            await _categoryLogic.AddCategoryAsync(_mapper.Map<Category>(category));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    
    [HttpPut]
    public async Task<IActionResult> UpdateCategory(CategoryView category)
    {
        try
        {
            await _categoryLogic.UpdateCategoryAsync(_mapper.Map<Category>(category));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveCategory(int id)
    {
        if ( await _categoryLogic.RemoveCategoryAsync(id))
        {
            return Ok();
        }
        else
        {
            return BadRequest("ObjectNotFound");
        }
    }
    
    [HttpPost]
    [Route("/api/addproductcategory")]
    public async Task<IActionResult> AddProductOrder(int id, int idProduct)
    {
        try
        {
            await _categoryLogic.AddProductAsync(id, idProduct);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
}