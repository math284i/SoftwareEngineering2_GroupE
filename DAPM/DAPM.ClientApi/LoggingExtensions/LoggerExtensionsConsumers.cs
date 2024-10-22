using Microsoft.Extensions.Logging;
namespace DAPM.ClientApi.LoggingExtensions;

public static partial class LoggerExtensionsConsumers
{
    // Collab Handshake
    [LoggerMessage(LogLevel.Information, "CollabHandshakeProcessResult received")]
    public static partial void CollabReceived(this ILogger logger);

    // Get Organizations
    [LoggerMessage(LogLevel.Information, "GetOrganizationsResultMessage received")]
    public static partial void GetOrganizationsReceived(this ILogger logger);

    // Get Pipeline Execution
    [LoggerMessage(LogLevel.Information, "GetPipelineExecutionStatusRequestResult received")]
    public static partial void GetPipelineReceived(this ILogger logger);

    // Get Pipeline process

    // Get Repositories

    // Get ResourceFiles

    // Get RessourceProcess

    // Post item

    // Post Pipeline

}