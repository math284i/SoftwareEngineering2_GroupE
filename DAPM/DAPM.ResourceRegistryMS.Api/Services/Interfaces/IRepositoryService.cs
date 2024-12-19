using DAPM.ResourceRegistryMS.Api.Models;
using RabbitMQLibrary.Models;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.ResourceRegistryMS.Api.Services.Interfaces
{
    public interface IRepositoryService
    {
        Task<Repository> GetRepositoryById(Guid organizationId, Guid repositoryId);
        Task<IEnumerable<Repository>> GetAllRepositories();
        Task<IEnumerable<Models.Resource>> GetResourcesOfRepository(Guid organizationId, Guid repositoryId);
        Task<IEnumerable<Models.Pipeline>> GetPipelinesOfRepository(Guid organizationId, Guid repositoryId);
        Task<Models.Pipeline> AddPipelineToRepository(Guid organizationId, Guid repositoryId, PipelineDTO pipeline);
        Task<bool> DeleteRepositoryById(Guid organizationId, Guid repositoryId);
    }
}
