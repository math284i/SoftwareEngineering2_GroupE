using Microsoft.Extensions.Logging;
namespace DAPM.ResourceRegistryMS.Api.LoggingExtensions;
/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */
public static partial class LoggerExtensionsConsumers
{
    // Get OrganizationsConsumer

    [LoggerMessage(LogLevel.Information, "Get OrganizationsMessage received")]
    public static partial void GetOrganizationMessageReceived(this ILogger logger);

    // GetPipelinesConsumer
    [LoggerMessage(LogLevel.Information, "GetPipelinesMessage received")]
    public static partial void GetPipelineMessageReceived(this ILogger logger);

    // GetRepositoriesConsumer
    [LoggerMessage(LogLevel.Information, "GetRepositoriesMessage received")]
    public static partial void GetRepositoriesMessageReceived(this ILogger logger);
    
    // GetResourcesConsumer
    [LoggerMessage(LogLevel.Information, "GetResourcesMessage received")]
    public static partial void GetResourcesMessageReceived(this ILogger logger);

    // PostPipeline
    [LoggerMessage(LogLevel.Information, "PostPipelineToRegistryMessage received")]
    public static partial void PostPipelineMessageReceived(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "PostPipelineToRegistryResultMessage published")]
    public static partial void PostPipelineMessagePublished(this ILogger logger);
    
    // PostRepository
    [LoggerMessage(LogLevel.Information, "PostRepositoryToRegistryMessage received")]
    public static partial void PostRepositoryMessageReceived(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "PostRepositoryToRegistryResultMessage published")]
    public static partial void PostRepositoryMessagePublished(this ILogger logger);
    
    // PostResource
    [LoggerMessage(LogLevel.Information, "PostResourceToRegistryMessage received")]
    public static partial void PostResourceMessageReceived(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "PostResourceToRegistryResultMessage published")]
    public static partial void PostResourceMessagePublished(this ILogger logger);
}