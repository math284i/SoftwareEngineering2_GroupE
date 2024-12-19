using DAPM.ResourceRegistryMS.Api.Models;

/**
 * All new changes are made by:
 * @Author: s216160
 */

namespace DAPM.ResourceRegistryMS.Api.Repositories.Interfaces
{
    public interface IPipelineRepository
    {
        public Task<Pipeline> AddPipeline(Pipeline pipeline);
        public Task<IEnumerable<Pipeline>> GetPipelinesFromRepository(Guid organizationId, Guid repositoryId);
        public Task<Pipeline> GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId);
        public Task<bool> DeletePipeline(Guid organizationId, Guid repositoryId, Guid pipelineId);
    }
}
