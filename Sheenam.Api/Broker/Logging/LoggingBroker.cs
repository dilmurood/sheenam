using Microsoft.Extensions.Logging;
using System;

namespace Sheenam.Api.Broker.Logging
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger)
        {
            this.logger = logger;
        }
        public void LogError(Exception exception) =>
            this.logger.LogError(exception, exception.Message);

        public void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception, exception.Message);

        public void LogTrace(string message) =>
            this.logger.LogTrace(message);
    }
}
