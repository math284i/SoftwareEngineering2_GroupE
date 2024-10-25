namespace DAPM.Orchestrator.LoggerExtensions;

public static partial class LoggerExtensionsProcesses
{
    // CollabHandshakeResponseProcess
    [LoggerMessage(LogLevel.Information, "HANDSHAKE RESPONSE PROCESS STARTED")]
    public static partial void ResponseHandshake(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "REGISTRY UPDATE RECEIVED")]
    public static partial void ResponseRegistry(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "HANDSHAKE ACK RECEIVED")]
    public static partial void ResponseHandshakeReceived(this ILogger logger);

    // CollabHandskakeProcess
    [LoggerMessage(LogLevel.Information, "HANDSHAKE STARTED")]
    public static partial void ProcessHandshake(this ILogger logger);
    
    [LoggerMessage(LogLevel.Information, "HANDSHAKE REQUEST RESPONSE RECEIVED")]
    public static partial void ProcessHandshakeReceived(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "REGISTRY UPDATE RECEIVED")]
    public static partial void ProcessRegistry(this ILogger logger);
    
    [LoggerMessage(LogLevel.Information, "REGISTRY UPDATE ACK RECEIVED")]
    public static partial void ProcessRegistryReceived(this ILogger logger);
}