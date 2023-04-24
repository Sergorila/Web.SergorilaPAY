using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BLL.Interfaces;
using Entities;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SergorilaPAY2._0.HangFire;
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
    
    [HttpGet]
    [Route("/api/getuserbylogin")]
    public async Task<IActionResult> GetUserByLogin(string login)
    {
        try
        {
            var user = await _userLogic.GetUserByLoginAsync(login);
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
    [Route("Login")]
    public async Task<IActionResult> LoginUser(string login, string password)
    {
        if (await _userLogic.CheckUserAsync(login, password))
        {
            var user = await _userLogic.GetUserByLoginAsync(login);
            var mappedUser = _mapper.Map<User>(user);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, mappedUser.Login.ToLower()),
                new Claim(ClaimTypes.Name, mappedUser.TelegramID.ToLower()),
            };
            
            ClaimsIdentity claimsIdentity = new(
                claims, 
                "Token", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: "MyAuthServer",
                audience: "MyAuthClient",
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(1)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("AMOGU$AB1GU$SUg0M4")),
                    SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                user_name = claimsIdentity.Name,
            };
            
            BackgroundJob.Enqueue(() => HangFireWorker.SendEmailAboutLoging(
                "sporeui@yandex.ru", 
                mappedUser.TelegramID));
            
            return Ok(response);
        }
        
        return Unauthorized();
    }
}