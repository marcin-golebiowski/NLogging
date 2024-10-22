using Microsoft.Extensions.Logging;


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(static builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("ConsoleApp1.Program", LogLevel.Debug)
                    .AddConsole();
            });

            ILogger logger = loggerFactory.CreateLogger<Program>();
            logger.LogDebug("Hello {Target}", "Everyone");
        }
    }
}
