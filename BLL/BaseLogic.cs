using DAL;
using Microsoft.Extensions.Logging;

namespace BLL;

public abstract class BaseLogic
{
    protected readonly ILogger Logger;

    protected BaseLogic(ILogger<BaseLogic> logger)
    {
        Logger = logger;
    }
}