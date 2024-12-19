namespace DAPM.OperatorMS.Api.LoggerExtensions;

/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */
public static partial class LoggerExtensionsConsumers
{
    // ExecuteOperatorMessageConsumer

    [LoggerMessage(LogLevel.Information, "ExecuteMinerMessage Received")]
    public static partial void ExecuteMessageReceived(this ILogger logger);

    // GetExecutionOutputMessageConsumer

    [LoggerMessage(LogLevel.Information, "GetExecutionOutputMessage Received")]
    public static partial void GetExecutionMessageReceived(this ILogger logger);

    // PostInputRessourceMessageConsumer

    [LoggerMessage(LogLevel.Information, "PostInputResourceMessage Received")]
    public static partial void PostInputMessageReceived(this ILogger logger);

}