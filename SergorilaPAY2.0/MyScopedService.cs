using DAL.Interfaces;

namespace SergorilaPAY2._0;

public class MyScopedService : IScopedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUserDao _userDao;

    public MyScopedService(IServiceProvider serviceProvider, IUserDao userDao)
    {
        _serviceProvider = serviceProvider;
        _userDao = userDao;
    }
}

public interface IScopedService
{
    
}