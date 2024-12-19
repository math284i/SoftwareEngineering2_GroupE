using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.ResourceRegistryMS.Api.Repositories
{
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly ResourceRegistryDbContext _context;
        private readonly ILogger<RepositoryRepository> _logger;

        public RepositoryRepository(ResourceRegistryDbContext context, ILogger<RepositoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Repository> PostRepository(Repository repository)
        {
            await _context.Repositories.AddAsync(repository);
            _context.SaveChanges();
            return repository;
        }

        public async Task<IEnumerable<Repository>> GetAllRepositories()
        {
            return await _context.Repositories.ToListAsync();
        }

        public async Task<IEnumerable<Repository>> GetRepositoriesOfOrganization(Guid organizationId)
        {
            return _context.Repositories.Where(r => r.PeerId == organizationId);
        }

        public async Task<Repository> GetRepositoryById(Guid organizationId, Guid repositoryId)
        {
            var repository = _context.Repositories.Include(r => r.Peer).Single(r => r.Id == repositoryId && r.PeerId == organizationId);

            if (repository == null)
            {
                return null;
            }

            return repository;
        }

        public async Task<bool> DeleteRepository(Guid organizationId, Guid repositoryId)
        {
            var repository = _context.Repositories.Include(r => r.Peer).Single(r => r.Id == repositoryId && r.PeerId == organizationId); 

            if (repository == null)
            {
                _logger.LogWarning($"Repository with ID {repositoryId} not found or does not belong to organization {organizationId}.");
                return false;
            }

            _context.Repositories.Remove(repository);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Repository with ID {repositoryId} deleted.");
            return true;
        }
    }
}
