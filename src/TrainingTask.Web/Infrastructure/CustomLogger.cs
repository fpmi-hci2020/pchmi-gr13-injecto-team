using System;
using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TrainingTask.Web.Infrastructure
{
    public class CustomLogger : ILogger
    {
        private static readonly object _sync = new object();

        private readonly IConfiguration _config;

        public CustomLogger(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                var path = _config["Logpath"];

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = Path.Combine(path,
                    $"{AppDomain.CurrentDomain.FriendlyName}_{DateTime.Now:dd.MM.yyy}.log");

                if (!(exception is null))
                {
                    var fullText = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] [{1}.{2}()] {3} {4}\r\n",
                        DateTime.Now, exception.TargetSite.DeclaringType, exception.TargetSite.Name,
                        exception.GetType().FullName, exception.Message);

                    lock (_sync)
                    {
                        File.AppendAllText(filename, fullText);
                    }
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}