using Microsoft.Extensions.Logging;
using System.Text.Json;

using ILoggerFactory loggerFactory =
    LoggerFactory.Create(builder =>
        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss ";
        }));
ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
using (logger.BeginScope("[scope is enabled]"))
{
    logger.LogInformation("Hello World!");
    using (logger.BeginScope("[dt]"))
    {
        logger.LogInformation("Logs contain timestamp and log level.");
        logger.LogInformation("Each log message is fit in a single line.");
    }
}


using ILoggerFactory loggerFactory2 =
    LoggerFactory.Create(builder =>
        builder.AddSystemdConsole(options =>
        {
            options.IncludeScopes = true;
            options.TimestampFormat = "HH:mm:ss ";
        }).AddJsonConsole(options =>
        {
            options.IncludeScopes = false;
            options.TimestampFormat = "HH:mm:ss ";
            options.JsonWriterOptions = new JsonWriterOptions
            {
                Indented = true
            };
        }));


ILogger<Program> logger2 = loggerFactory2.CreateLogger<Program>();
using (logger2.BeginScope("[scope is enabled]"))
{
    logger2.LogInformation("Hello World!");
    using (logger.BeginScope("[dt]"))
    {
        logger2.LogInformation("Logs contain timestamp and log level.");
        logger2.LogInformation("Each log message is fit in a single line.");
    }
}
