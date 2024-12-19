using DAPM.RepositoryMS.Api.Models.PostgreSQL;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.RepositoryMS.Api.Repositories.Interfaces
{
    public interface IRepositoryRepository
    {
        Task<Repository> GetRepositoryById(Guid repositoryId);
        Task<Repository> CreateRepository(string name);
        Task<bool> DeleteRepository(Guid organizationId, Guid repositoryId);
    }
}
