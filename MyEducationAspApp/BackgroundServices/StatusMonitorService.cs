using System.Globalization;
using Microsoft.AspNetCore.SignalR;
using MyEducationAspApp.Hubs;

namespace MyEducationAspApp.BackgroundServices;

public class StatusMonitorService : BackgroundService
{
    private const string GetUsedRam = "free --kilo|awk 'NR==2 {print $3}'";
    private const string GetUsedCpu = "vmstat 1 2|tail -1|awk '{print $15}'";
    private readonly ILogger<StatusMonitorService> _logger;
    private readonly IHubContext<StatusHub> _hubContext;
    private readonly string _totalRam;

    public StatusMonitorService(ILogger<StatusMonitorService> logger, IHubContext<StatusHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
        var ram = double.Parse(BushExecutor.Execute("free --kilo|awk 'NR==2 {print $2}'"));
        ram /= 1048576d; //ToGB
        ram = Math.Round(ram, 2);
        _totalRam = ram.ToString(CultureInfo.InvariantCulture);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var stats = GetStats();

            await _hubContext.Clients.All.SendAsync("ReceiveStatus", stats, cancellationToken: stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }
    }

    private string GetStats()
    {
        var usedRam = BushExecutor.Execute(GetUsedRam);
        var usedCpu = BushExecutor.Execute(GetUsedCpu);

        return $"Used CPU: {usedCpu}%\nUsed RAM: {usedRam} GB/{_totalRam} GB";
    }
}
