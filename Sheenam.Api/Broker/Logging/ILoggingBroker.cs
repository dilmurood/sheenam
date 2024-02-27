using System;

namespace Sheenam.Api.Broker.Logging
{
    public interface ILoggingBroker
    {
        void LogError(Exception exception);
        void LogCritical(Exception exception);

        void LogTrace(string message);
    }
}
