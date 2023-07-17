using Microsoft.AspNetCore.SignalR;
using MyEducationAspApp.Hubs;

namespace MyEducationAspApp.BackgroundServices;

public class StatusMonitorService : BackgroundService
{
    private readonly ILogger<StatusMonitorService> _logger;
    private readonly IHubContext<StatusHub> _hubContext;

    public StatusMonitorService(ILogger<StatusMonitorService> logger, IHubContext<StatusHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var freeRam = GetFreeRam();
            
            await _hubContext.Clients.All.SendAsync("ReceiveStatus", freeRam, cancellationToken: stoppingToken);
            
            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }
    }

    private string GetFreeRam()
    {
        return "512 MB" + Random.Shared.NextInt64();
    }
}
