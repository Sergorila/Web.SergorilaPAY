using AutoMapper;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SergorilaPAY2._0.Views;

namespace SergorilaPAY2._0.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderLogic _orderLogic;
    private readonly IMapper _mapper;
    
    public OrderController(IOrderLogic orderLogic, IMapper mapper)
    {
        _orderLogic = orderLogic;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("/api/getorder")]
    public async Task<IActionResult> GetOrder(int id)
    {
        try
        {
            var order = await _orderLogic.GetOrderAsync(id);
            if (order != null)
            {
                return Ok(order);
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
    [Route("/api/addorder")]
    public async Task<IActionResult> AddOrder(OrderView order)
    {
        try
        {
            await _orderLogic.AddOrderAsync(_mapper.Map<Order>(order));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpPost]
    [Route("/api/addproductorder")]
    public async Task<IActionResult> AddProductOrder(int id, int idProduct)
    {
        try
        {
            await _orderLogic.AddProductAsync(id, idProduct);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpGet]
    [Route("/api/checkorder")]
    public async Task<IActionResult> CheckOrder(int id)
    {
        try
        {
            var res =  await _orderLogic.CheckOrderAsync(id);
            if (res)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateOrder(OrderView order)
    {
        try
        {
            await _orderLogic.UpdateOrderAsync(_mapper.Map<Order>(order));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveOrder(int id)
    {
        if ( await _orderLogic.RemoveOrderAsync(id))
        {
            return Ok();
        }
        else
        {
            return BadRequest("ObjectNotFound");
        }
    }
    
    [HttpGet]
    [Route("api/getorderitems")]
    public async Task<IActionResult> GetOrderItems(int id, int offset, int limit)
    {
        try
        {
            var order = _orderLogic.GetOrderItemsAsync(id, offset, limit);
            if (order != null)
            {
                return Ok(order);
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
}