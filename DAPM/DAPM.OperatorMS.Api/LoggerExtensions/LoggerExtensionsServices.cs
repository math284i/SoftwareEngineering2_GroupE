namespace DAPM.OperatorMS.Api.LoggerExtensions;

public static partial class LoggerExtensionsServices
{
    // DockerService
    [LoggerMessage(LogLevel.Information, "INPUT RESOURCE POSTED {Id} {Name} {Length} IN PATH {filePath}")]
    public static partial void DockerResourcePosted(this ILogger logger, Guid id, string name, int length,
        string filePath);

    [LoggerMessage(LogLevel.Error, "An error occurred: {Exception}")]
    public static partial void DockerFailed(this ILogger logger, Exception exception);

}