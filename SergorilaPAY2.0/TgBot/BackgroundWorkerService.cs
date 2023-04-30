using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Options;

namespace SergorilaPAY2._0;

public class BackgroundWorkerService : BackgroundService
{
    private readonly IHostApplicationLifetime _lifetime;
    private readonly IServiceProvider _serviceProvider;

    public BackgroundWorkerService(IServiceProvider serviceProvider, IHostApplicationLifetime lifetime)
    {
        _serviceProvider = serviceProvider;
        _lifetime = lifetime;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var userdao = scope.ServiceProvider.GetRequiredService<IUserDao>();
            var telegramBot = new TelegramBot(userdao);
                
            telegramBot.StartPolling();
        }
            
        //await Task.Delay(1000, stoppingToken);
    }
    
    static async Task<bool> WaitForAppStartup(IHostApplicationLifetime lifetime, CancellationToken stoppingToken)
    {
        var startedSource = new TaskCompletionSource();
        using var reg1 = lifetime.ApplicationStarted.Register(() => startedSource.SetResult());
        
        var cancelledSource = new TaskCompletionSource();
        using var reg2 = stoppingToken.Register(() => cancelledSource.SetResult());
        
        Task completedTask = await Task.WhenAny(startedSource.Task, cancelledSource.Task).ConfigureAwait(false);
        
        return completedTask == startedSource.Task;
    }
}