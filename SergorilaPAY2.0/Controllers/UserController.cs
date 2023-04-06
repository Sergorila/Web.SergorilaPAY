using AutoMapper;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SergorilaPAY2._0.Views;

namespace SergorilaPAY2._0.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IMapper _mapper;
    
    public UserController(IUserLogic userlogic, IMapper mapper)
    {
        _userLogic = userlogic;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var res = await _userLogic.GetUserAsync(id);
                
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
    }
}