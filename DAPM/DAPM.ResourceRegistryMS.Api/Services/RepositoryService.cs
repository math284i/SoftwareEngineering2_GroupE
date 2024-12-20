﻿using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;
using DAPM.ResourceRegistryMS.Api.Services.Interfaces;

/**
 * All new changes are made by:
 * @Author: s204197, s204166, s204178
 */
 
namespace DAPM.ResourceRegistryMS.Api.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ILogger<IRepositoryService> _logger;
        private IResourceRepository _resourceRepository;
        private IRepositoryRepository _repositoryRepository;
        private IPeerRepository _peerRepository;
        private IPipelineRepository _pipelineRepository;

        public RepositoryService(ILogger<IRepositoryService> logger, 
            IRepositoryRepository repositoryRepository, 
            IPeerRepository peerRepository,
            IResourceRepository resourceRepository,
            IPipelineRepository pipelineRepository)
        {
            _repositoryRepository = repositoryRepository;
            _pipelineRepository = pipelineRepository;
            _peerRepository = peerRepository;
            _resourceRepository = resourceRepository;
            _logger = logger;
        }

        public async Task<Pipeline> AddPipelineToRepository(Guid organizationId, Guid repositoryId, RabbitMQLibrary.Models.PipelineDTO pipeline)
        {
            _logger.Log(LogLevel.Information, "Inserting pipeline(before): " + pipeline.Timestamp.ToString());
            var pipelineToInsert = new Pipeline()
            {
                Id = pipeline.Id,
                RepositoryId = pipeline.RepositoryId,
                PeerId = pipeline.OrganizationId,
                Name = pipeline.Name + "/" + pipeline.Timestamp.ToString(),
            };
            _logger.Log(LogLevel.Information, "Inserting pipeline(after): " + pipelineToInsert.Name);

            return await _pipelineRepository.AddPipeline(pipelineToInsert);
        }


        public async Task<IEnumerable<Repository>> GetAllRepositories()
        {
            return await _repositoryRepository.GetAllRepositories();
        }

        public async Task<IEnumerable<Pipeline>> GetPipelinesOfRepository(Guid organizationId, Guid repositoryId)
        {
            return await _pipelineRepository.GetPipelinesFromRepository(organizationId, repositoryId);
        }

        public async Task<Repository> GetRepositoryById(Guid organizationId, Guid repositoryId)
        {
            return await _repositoryRepository.GetRepositoryById(organizationId, repositoryId);
        }

        public async Task<IEnumerable<Resource>> GetResourcesOfRepository(Guid organizationId, Guid repositoryId)
        {
            return _resourceRepository.GetResourcesOfRepository(organizationId, repositoryId);
        }
        public async Task<bool> DeleteRepositoryById(Guid organizationId, Guid repositoryId)
        {
            return await _repositoryRepository.DeleteRepository(organizationId, repositoryId);
        }
    }
}
