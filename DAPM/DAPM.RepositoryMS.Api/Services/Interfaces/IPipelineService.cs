using DAPM.RepositoryMS.Api.Models.PostgreSQL;

/**
 * All new changes are made by:
 * @Author: s216160
 */

namespace DAPM.RepositoryMS.Api.Services.Interfaces
{
    public interface IPipelineService
    {
        Task<Pipeline> GetPipelineById(Guid repositoryId, Guid pipelineId);
        Task<bool> DeletePipeline(Guid organizationId, Guid repositoryId, Guid pipelineId);
    }
}
