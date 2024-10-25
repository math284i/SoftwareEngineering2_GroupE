namespace DAPM.Orchestrator.LoggerExtensions;

public static partial class LoggerExtensionsOrchestrator
{
    // OrchestratorEngine
    [LoggerMessage(LogLevel.Information, "ORCHESTRATOR ENGINE removing process with id {ProcessId}")]
    public static partial void OrchestratorDeleting(this ILogger logger, Guid processId);
}