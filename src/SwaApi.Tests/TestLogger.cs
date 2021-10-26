using System;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace SwaApi.Tests
{
    public class TestLogger : ILogger
    {
        private readonly ITestOutputHelper _outputHelper;

        public TestLogger(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            _outputHelper.WriteLine(message);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }
    }
}