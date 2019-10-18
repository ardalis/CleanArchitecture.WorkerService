using System;

namespace CleanArchitecture.Core.Interfaces
{
    // Helps if you need to confirm logging is happening
    // https://ardalis.com/testing-logging-in-aspnet-core
    public interface ILoggerAdapter<T>
    {
        void LogInformation(string message, params object[] args);
        void LogError(Exception ex, string message, params object[] args);
    }
}
