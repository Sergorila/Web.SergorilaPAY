using Entities;

namespace BLL.Interfaces;

public interface IUserLogic
{
    Task<User> GetUserAsync(int id);
    Task<bool> CheckUserAsync(string login, string password);
    Task AddUserAsync(User user);
    Task<bool> RemoveUserAsync(int id);
    Task UpdateUserAsync(User user);
    Task<User> GetUserTGAsync(string tgId);
}