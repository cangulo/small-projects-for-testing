using Amazon.Lambda.Core;
using Microsoft.Extensions.Logging;
using System;

namespace DatabaseMigrationsHandler.Lambda.Logger
{
    public class LambdaLoggerWrapper<T> : ILogger<T>
    {
        private readonly ILambdaLogger _lambdaLogger;

        public LambdaLoggerWrapper(ILambdaContext lambdaContext)
        {
            _lambdaLogger = lambdaContext.Logger ?? throw new ArgumentNullException(nameof(lambdaContext));
        }

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _lambdaLogger.LogLine($"{formatter(state, exception)}");
        }
    }
}