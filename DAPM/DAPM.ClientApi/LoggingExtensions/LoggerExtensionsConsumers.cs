using Microsoft.Extensions.Logging;
namespace DAPM.ClientApi.LoggingExtensions;
/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */
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
    [LoggerMessage(LogLevel.Information, "GetPipelinesProcessResult received")]
    public static partial void GetPipelineProcessReceived(this ILogger logger);

    // Get Repositories
    [LoggerMessage(LogLevel.Information, "GetRepositoriesProcessResult received")]
    public static partial void GetRepositoriesReceived(this ILogger logger);

    // Get ResourceFiles
    [LoggerMessage(LogLevel.Information, "GetResourceFilesProcessResult received")]
    public static partial void GetResourceFilesReceived(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "FILE NAME {fileName}")]
    public static partial void FileName(this ILogger logger, string fileName);

    // Get RessourceProcess
    [LoggerMessage(LogLevel.Information, "GetResourcesProcessResult received")]
    public static partial void GetRessourceProcessReceived(this ILogger logger);

    // Post item
    [LoggerMessage(LogLevel.Information, "CreateNewItemResultMessage received")]
    public static partial void PostItemReceived(this ILogger logger);

    // Post Pipeline
    [LoggerMessage(LogLevel.Information, "PostPipelineCommandProcessResult received")]
    public static partial void PostPipelineRecieved(this ILogger logger);
}