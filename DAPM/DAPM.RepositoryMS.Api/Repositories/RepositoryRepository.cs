using DAPM.RepositoryMS.Api.Data;
using DAPM.RepositoryMS.Api.Models.PostgreSQL;
using DAPM.RepositoryMS.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.RepositoryMS.Api.Repositories
{
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly RepositoryDbContext _context;

        public RepositoryRepository(RepositoryDbContext context)
        {
            _context = context;
        }

        public async Task<Repository> CreateRepository(string name)
        {
            Repository repository = new Repository() { Name = name };
            await _context.Repositories.AddAsync(repository);
            _context.SaveChanges();
            return repository;
        }

        public async Task<Repository> GetRepositoryById(Guid repositoryId)
        {
            return await _context.Repositories.FirstOrDefaultAsync(r => r.Id == repositoryId);
        }
        public async Task<bool> DeleteRepository(Guid organizationId, Guid repositoryId)
        {
            var repository = await _context.Repositories.FirstOrDefaultAsync(r => r.Id == repositoryId);

            if (repository == null)
            {
                // Repository not found or does not belong to the organization
                return false;
            }

            _context.Repositories.Remove(repository);
            _context.SaveChanges();

            return true;
        }
    }
}
