using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace cangulo.DbMigrationsHandler.UT.FakeLogger
{
    public class FakeLogger<T> : ILogger<T>
    {
        private readonly ITestOutputHelper _outputHelper;

        public FakeLogger(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _outputHelper.WriteLine($"{DateTime.Now.TimeOfDay}--{typeof(T).Name}-->{state}");
        }
    }

    internal class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();

        private NullScope()
        {
        }

        public void Dispose()
        {
        }
    }
}