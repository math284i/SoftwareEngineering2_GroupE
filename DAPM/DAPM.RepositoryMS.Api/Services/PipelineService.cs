using DAPM.RepositoryMS.Api.Models.PostgreSQL;
using DAPM.RepositoryMS.Api.Repositories.Interfaces;
using DAPM.RepositoryMS.Api.Services.Interfaces;

namespace DAPM.RepositoryMS.Api.Services
{
    public class PipelineService : IPipelineService
    {
        private IPipelineRepository _pipelineRepository;
        public PipelineService(IPipelineRepository pipelineRepository) 
        {
            _pipelineRepository = pipelineRepository;
        }
        public Task<Pipeline> GetPipelineById(Guid repositoryId, Guid pipelineId)
        {
            return _pipelineRepository.GetPipelineById(repositoryId, pipelineId);
        }
        public async Task<bool> DeletePipeline(Guid organizationId, Guid repositoryId, Guid pipelineId)
        {
            return await _pipelineRepository.DeletePipeline(organizationId, repositoryId, pipelineId);
        }
    }
}
