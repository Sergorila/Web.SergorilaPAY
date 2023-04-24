using DAL.Interfaces;
using BLL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace BLL;

public class UserLogic : BaseLogic, IUserLogic
{
    private readonly IUserDao _dao;
    
    public UserLogic(ILogger<BaseLogic> logger, IUserDao dao) : base(logger)
    {
        _dao = dao;
    }

    public async Task<User> GetUserAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying get user by ID: {Id}", id);
            
            var res = await _dao.GetUserAsync(id);
            
            Logger.LogInformation("Compete getting user by ID: {Id}", id);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while getting user by ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> CheckUserAsync(string login, string password)
    {
        try
        {
            Logger.LogInformation("Trying check user by login: {Login}", login);
            var isExist = await _dao.CheckUserAsync(login, password);
            Logger.LogInformation("Complete checking user by login: {Login}", login);
            return isExist;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while checking user by login: {Login}", login);
            throw;
        }
    }

    public async Task AddUserAsync(User user)
    {
        try
        {            
            Logger.LogInformation("Trying add user: {Login}", user.Login);
            await _dao.AddUserAsync(user);
            Logger.LogInformation("Complete adding user: {Login}", user.Login);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while adding user: {Login}", user.Login);
            throw;
        }
    }

    public async Task<bool> RemoveUserAsync(int id)
    {
        try
        {
            Logger.LogInformation("Trying remove user by ID: {Id}", id);
            var isRemoved = await _dao.RemoveUserAsync(id);
            Logger.LogInformation("Complete removing user by ID: {Id}", id);
            return isRemoved;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while removing user by ID: {Id}", id);
            throw;
        }
    }

    public async Task UpdateUserAsync(User user)
    {
        try
        {
            Logger.LogInformation("Trying update user: {Id}", user.Id);
            
            await _dao.UpdateUserAsync(user);
            Logger.LogInformation("Complete updating user: {Id}", user.Id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while updating user: {Id}", user.Id);
            throw;
        }
    }

    public async Task<User> GetUserTGAsync(string tgId)
    {
        try
        {
            Logger.LogInformation("Trying get user by tgID: {Id}", tgId);
            
            var res = await _dao.GetUserTGAsync(tgId);
            
            Logger.LogInformation("Compete getting user by tgID: {Id}", tgId);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while getting user by tgID: {Id}", tgId);
            throw;
        }
    }

    public async Task<User> GetUserByLoginAsync(string login)
    {
        try
        {
            Logger.LogInformation("Trying get user by login: {login}", login);
            
            var res = await _dao.GetUserByLoginAsync(login);
            
            Logger.LogInformation("Compete getting user by login: {login}", login);
            return res;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, 
                "Error while getting user by login: {login}", login);
            throw;
        }
    }
}