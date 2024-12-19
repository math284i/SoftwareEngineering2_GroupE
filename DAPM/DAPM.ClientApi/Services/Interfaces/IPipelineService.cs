namespace DAPM.ClientApi.Services.Interfaces
{
/**
 * All new changes are made by:
 * @Author: s216160
 */
    public interface IPipelineService
    {
        public Guid GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId);
        public Guid CreatePipelineExecution(Guid organizationId, Guid repositoryId, Guid pipelineId);
        public Guid PostStartCommand(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId);
        public Guid GetExecutionStatus(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId);
        public Guid DeletePipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId);
    }
}
