using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SwaApi.Tests
{
    public class ListLogger : ILogger
    {
        public readonly IList<string> Logs;

        public ListLogger()
        {
            Logs = new List<string>();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            Logs.Add(message);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }
    }
    internal class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();

        private NullScope()
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }

}