namespace DAPM.ClientApi.LoggingExtensions;
/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */
public static partial class LoggerExtensionsServices
{
    // Organization
    [LoggerMessage(LogLevel.Debug, "GetOrganizationByIdMessage Enqueued")]
    public static partial void OrganizationByIdEnqueued(this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "GetOrganizationsMessage Enqueued")]
    public static partial void OrganizationMessageEnqueued(this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "GetRepositoriesRequest Enqueued")]
    public static partial void OrganizationGetRepositoryEnqueued(this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "PostRepositoryRequest Enqueued")]
    public static partial void OrganizationPostRepositoryEnqueued(this ILogger logger);

    // Pipeline
    [LoggerMessage(LogLevel.Debug, "CreatePipelineExecutionRequest Enqueued")]
    public static partial void PipelineCreateEnqueued(this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "GetPipelineExecutionStatus Enqueued")]
    public static partial void PipelineGetExecutionEnqueued(this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "GetPipelinesRequest Enqueued")]
    public static partial void PipelineGetPipelineEnqueued(this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "PipelineStartCommandRequest Enqueued")]
    public static partial void PipelineStartEnqueued(this ILogger logger);

    // Repository
    [LoggerMessage(LogLevel.Debug, "GetRepositoriesRequest Enqueued")]
    public static partial void RepositoryGetRepositoriesEnqueued(this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "GetResourcesRequest Enqueued")]
    public static partial void RepositoryGetResourcesEnqueued(this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "GetPipelinesRequest Enqueued")]
    public static partial void RepositoryGetPipelineEnqueued(this ILogger logger);
    
    [LoggerMessage(LogLevel.Debug, "PostPipelineToRepoMessage Enqueued")]
    public static partial void RepositoryPostPipelineEnqueued(this ILogger logger);
    
    [LoggerMessage(LogLevel.Debug, "PostResourceRequest Enqueued")]
    public static partial void RepositoryPostResourceEnqueued(this ILogger logger);

    // Resource
    [LoggerMessage(LogLevel.Debug, "GetResourcesRequest Enqueued")]
    public static partial void ResourceGetEnqueued(this ILogger logger);

    [LoggerMessage(LogLevel.Debug, "GetResourceFilesRequest Enqueued")]
    public static partial void ResourceGetFilesEnqueued(this ILogger logger);
    
    // System
    [LoggerMessage(LogLevel.Debug, "CollabHandshakeRequest Enqueued")]
    public static partial void SystemCollabEnqueued(this ILogger logger);

    // Ticket
    [LoggerMessage(LogLevel.Information, "A ticket status was updated for ticket {ticketId} but it didn't exist")]
    public static partial void TicketStatusUpdated(this ILogger logger, Guid ticketId);

    [LoggerMessage(LogLevel.Information, "A new ticket has been created")]
    public static partial void TicketCreated(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "A ticket resolution was updated for ticket {ticketId} but it didn't exist")]
    public static partial void TicketResolutionUpdated(this ILogger logger, Guid ticketId);

    [LoggerMessage(LogLevel.Information, "Ticket resolution of ticket {ticketId} has been updated")]
    public static partial void TicketResolution(this ILogger logger, Guid ticketId);
    
}