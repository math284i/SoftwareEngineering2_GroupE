using DAPM.ResourceRegistryMS.Api.Models;

/**
 * All new changes are made by:
 * @Author: s216160
 */

namespace DAPM.ResourceRegistryMS.Api.Services.Interfaces
{
    public interface IPipelineService
    {
        Task<Pipeline> GetPipelineById(Guid organizationId, Guid repositoryId, Guid resourceId);
        Task<bool> DeletePipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId);
    }
}
