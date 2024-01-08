namespace CleanArch.Application.Interfaces.Logging;

public interface IAppLogger<T>
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(Exception? exception = null, string? message = null, params object[] args);
    void LogCritical(Exception exception, string message, params object[] args);
}
