using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders(); // Remove default logging providers
                logging.AddConsole(); // Add console logging
                logging.AddDebug(); // Add debug logging
                logging.AddEventSourceLogger(); // Add event source logging
            })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<TimedHostedService>();
            });

        var host = builder.Build();
        host.Run();
    }
}

public class TimedHostedService : IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<TimedHostedService> _logger;
    private Timer? _timer = null;

    /*public TimedHostedService(ILogger<TimedHostedService> logger)
    {
        _logger = logger;
    }*/

    public TimedHostedService(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<TimedHostedService>();
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);

        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
