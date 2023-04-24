using Entities;
using DAL.Interfaces;
using DAL.DBContext;
using Microsoft.EntityFrameworkCore;
namespace DAL;

public class UserDao : BaseDao, IUserDao
{
    public UserDao(NpgsqlContext dbContext) : base(dbContext)
    {
    }

    public async Task<User> GetUserAsync(int id)
    {
        var user = await DbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id);
        
        return user;
    }

    public async Task<User> GetUserByLoginAsync(string login)
    {
        var user = await DbContext.Users
            .FirstOrDefaultAsync(u => u.Login == login);
        
        return user;
    }

    public async Task<bool> CheckUserAsync(string login, string password)
    {
        var user = await DbContext.Users
            .FirstOrDefaultAsync(u => 
                u.Login == login
                && u.Password == password);
        
        return user != null;
    }

    public async Task AddUserAsync(User user)
    {
        await DbContext.AddAsync(user);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> RemoveUserAsync(int id)
    {
        var user = await GetUserAsync(id);
        DbContext.Users.Remove(user);
        var entitiesCnt = await DbContext.SaveChangesAsync();
        
        return entitiesCnt != 0;
    }

    public async Task UpdateUserAsync(User user)
    {
        DbContext.Users.Update(user);
        await DbContext.SaveChangesAsync();
    }

    public async Task<User> GetUserTGAsync(string tgId)
    {
        var user = await DbContext.Users
            .FirstOrDefaultAsync(u => u.TelegramID == tgId);
        
        return user;
    }
}