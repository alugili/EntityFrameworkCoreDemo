using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCoreDemo
{
  public class ConsoleLoggerProvider : ILoggerProvider
  {
    // Todo Console Logger
    private static SmartInspectLogger _consoleLogger;

    public ILogger CreateLogger(string categoryName)
    {
      if (_consoleLogger != null)
      {
        return _consoleLogger;
      }

      return _consoleLogger = new SmartInspectLogger();
    }

    public void Dispose() { }

    private class SmartInspectLogger : ILogger
    {
      private string _lastMessage = string.Empty;

      public bool IsEnabled(LogLevel logLevel)
      {
        return true;
      }

      public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
      {
        if (eventId.Id == RelationalEventId.CommandExecuted.Id)
        {
          var data = state as IEnumerable<KeyValuePair<string, object>>;
          if (data != null)
          {
            var commandText = data.Single(p => p.Key == "commandText").Value.ToString();
            if (_lastMessage != commandText)
            {
              Console.WriteLine(commandText);
              _lastMessage = commandText;
            }
          }
        }
      }

      public IDisposable BeginScope<TState>(TState state)
      {
        return null;
      }
    }
  }

  public static class DbContextExtensions
  {
    public static void AddSmartInspectLogs(this DbContext dbContext)
    {
      var serviceProvider = dbContext.GetInfrastructure();
      var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
      loggerFactory.AddProvider(new ConsoleLoggerProvider());
    }
  }
}