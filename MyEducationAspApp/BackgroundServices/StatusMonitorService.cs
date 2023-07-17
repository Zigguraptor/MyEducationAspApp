using System.Globalization;
using Microsoft.AspNetCore.SignalR;
using MyEducationAspApp.Hubs;

namespace MyEducationAspApp.BackgroundServices;

public class StatusMonitorService : BackgroundService
{
    private const string GetUsedRam = "free --kilo|awk 'NR==2 {print $3}'";
    private const string GetUsedCpu = "vmstat 1 2|tail -1|awk '{print $15}'";
    private const double KbInGb = 1048576d;
    private readonly ILogger<StatusMonitorService> _logger;
    private readonly IHubContext<StatusHub> _hubContext;
    private readonly string _totalRam;

    public StatusMonitorService(ILogger<StatusMonitorService> logger, IHubContext<StatusHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
        var ram = double.Parse(BushExecutor.Execute("free --kilo|awk 'NR==2 {print $2}'"));
        ram = ConvertKbToGb(ram);
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
        string usedRam;
        string usedCpu;
        try
        {
            usedRam = BushExecutor.Execute(GetUsedRam);
            usedCpu = BushExecutor.Execute(GetUsedCpu);
        }
        catch (Exception e)
        {
            _logger.LogError("Bash execution error\n{e}", e);
            return "";
        }

        if (!double.TryParse(usedRam, out var ramInGb) || !int.TryParse(usedCpu, out var cpuPercents))
            return "";

        ramInGb = ConvertKbToGb(ramInGb);
        cpuPercents = 100 - cpuPercents;

        var serverTime = DateTime.Now;
        return
            $"Used CPU: {cpuPercents}%\nUsed RAM: {ramInGb} GB/{_totalRam} GB\nServer time: {serverTime.ToString(CultureInfo.InvariantCulture)}";
    }

    private static double ConvertKbToGb(double kilobits)
    {
        return Math.Round(kilobits / KbInGb, 2);
    }
}
