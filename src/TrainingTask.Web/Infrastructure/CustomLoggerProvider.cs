using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TrainingTask.Web.Infrastructure
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly IConfiguration _config;

        public CustomLoggerProvider(IConfiguration config)
        {
            _config = config;
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(_config);
        }
    }
}