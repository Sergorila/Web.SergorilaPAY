using AutoMapper;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SergorilaPAY2._0.Views;

namespace SergorilaPAY2._0.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductLogic _productLogic;
    private readonly IMapper _mapper;
    
    public ProductController(IProductLogic productLogic, IMapper mapper)
    {
        _productLogic = productLogic;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProduct(int id)
    {
        try
        {
            var product = await _productLogic.GetProductAsync(id);
            if (product != null)
            {
                var res = _mapper.Map<Product>(product);
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
    
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductView product)
    {
        try
        {
            await _productLogic.AddProductAsync(_mapper.Map<Product>(product));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductView product)
    {
        try
        {
            await _productLogic.UpdateProductAsync(_mapper.Map<Product>(product));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveProduct(int id)
    {
        if ( await _productLogic.RemoveProductAsync(id))
        {
            return Ok();
        }
        else
        {
            return BadRequest("ObjectNotFound");
        }
    }
}