namespace DAPM.Orchestrator.LoggerExtensions;
/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */
public static partial class LoggerExtensionsOrchestrator
{
    // OrchestratorEngine
    [LoggerMessage(LogLevel.Information, "ORCHESTRATOR ENGINE removing process with id {ProcessId}")]
    public static partial void OrchestratorDeleting(this ILogger logger, Guid processId);
}