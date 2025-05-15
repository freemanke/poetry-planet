using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PoetryPlanet.Services;

public class CustomFileLoggerProvider : ILoggerProvider
{
    private readonly StreamWriter logFileWriter;

    public CustomFileLoggerProvider(StreamWriter logFileWriter)
    {
        this.logFileWriter = logFileWriter ?? throw new ArgumentNullException(nameof(logFileWriter));
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new CustomFileLogger(categoryName, logFileWriter);
    }

    public void Dispose()
    {
        logFileWriter.Dispose();
    }
}

public class CustomFileLogger : ILogger
{
    private readonly string categoryName;
    private readonly StreamWriter logFileWriter;

    public CustomFileLogger(string categoryName, StreamWriter logFileWriter)
    {
        this.categoryName = categoryName;
        this.logFileWriter = logFileWriter;
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= LogLevel.Information;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
        var message = formatter(state, exception);
        logFileWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {logLevel.ToString().ToLower()[..4]} [{categoryName.Split(".").Last()}] {message}");
        logFileWriter.Flush();
    }
}
