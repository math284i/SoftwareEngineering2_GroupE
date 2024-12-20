﻿using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;
using DAPM.ResourceRegistryMS.Api.Services.Interfaces;

/**
 * All new changes are made by:
 * @Author: s216160
 */

namespace DAPM.ResourceRegistryMS.Api.Services
{
    public class PipelineService : IPipelineService
    {
        private IPipelineRepository _pipelineRepository;

        public PipelineService(IPipelineRepository pipelineRepository) 
        {
            _pipelineRepository = pipelineRepository;
        }   
        
        public async Task<Pipeline> GetPipelineById(Guid organizationId, Guid repositoryId, Guid resourceId)
        {
            return await _pipelineRepository.GetPipelineById(organizationId, repositoryId, resourceId);
        }
        public async Task<bool> DeletePipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
           return await _pipelineRepository.DeletePipeline(organizationId, repositoryId, pipelineId);
        }
    }
}
