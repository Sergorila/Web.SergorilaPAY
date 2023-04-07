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
    [Route("/api/getuser")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var user = await _userLogic.GetUserAsync(id);
            if (user != null)
            {
                var res = _mapper.Map<User>(user);
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
    public async Task<IActionResult> AddUser(UserView user)
    {
        try
        {
            await _userLogic.AddUserAsync(_mapper.Map<User>(user));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }

    [HttpGet]
    [Route("/api/checkuser")]
    public async Task<IActionResult> CheckUser(string login, string password)
    {
        try
        {
            var res =  await _userLogic.CheckUserAsync(login, password);
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
    public async Task<IActionResult> UpdateUser(UserView user)
    {
        try
        {
            await _userLogic.UpdateUserAsync(_mapper.Map<User>(user));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Bad request.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveUser(int id)
    {
        if ( await _userLogic.RemoveUserAsync(id))
        {
            return Ok();
        }
        else
        {
            return BadRequest("ObjectNotFound");
        }
    }
}