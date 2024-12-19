using DAPM.RepositoryMS.Api.Models.PostgreSQL;
using DAPM.RepositoryMS.Api.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Models;

/**
 * All new changes are made by:
 * @Author: s20416 and s204178
 */

namespace DAPM.RepositoryMS.Api.Consumers
{
    public class GetPipelinesFromRepoConsumer : IQueueConsumer<GetPipelinesFromRepoMessage>
    {
        private ILogger<GetPipelinesFromRepoConsumer> _logger;
        private IPipelineService _pipelineService;
        private IRepositoryService _repositoryService;
        private IQueueProducer<GetPipelinesFromRepoResultMessage> _getPipelinesFromRepoResultProducer;

        public GetPipelinesFromRepoConsumer(ILogger<GetPipelinesFromRepoConsumer> logger, IPipelineService pipelineService,
            IRepositoryService repositoryService, IQueueProducer<GetPipelinesFromRepoResultMessage> getPipelinesFromRepoResultProducer)
        {
            _logger = logger;
            _pipelineService = pipelineService;
            _repositoryService = repositoryService;
            _getPipelinesFromRepoResultProducer = getPipelinesFromRepoResultProducer;
        }

        public async Task ConsumeAsync(GetPipelinesFromRepoMessage message)
        {
            _logger.LogInformation("GetPipelinesFromRepoMessage received");
            var pipelines = Enumerable.Empty<Models.PostgreSQL.Pipeline>();

            if (message.PipelineId != null)
            {
                var pipeline = await _pipelineService.GetPipelineById(message.RepositoryId, (Guid)message.PipelineId);
                pipelines = pipelines.Append(pipeline);

            }
            else
            {
                pipelines = await _repositoryService.GetPipelinesFromRepository(message.RepositoryId);
            }

            var pipelinesDTOs = Enumerable.Empty<PipelineDTO>();

            foreach (var pipeline in pipelines)
            {
                var pipelineJson = JsonConvert.DeserializeObject<RabbitMQLibrary.Models.Pipeline>(pipeline.PipelineJson);
                var dto = new PipelineDTO()
                {
                    RepositoryId = pipeline.RepositoryId,
                    Id = pipeline.Id,
                    Name = pipeline.Name,
                    Timestamp = pipelineJson.Timestamp,
                    Pipeline = pipelineJson
                };
                pipelinesDTOs = pipelinesDTOs.Append(dto);
            }


            var resultMessage = new GetPipelinesFromRepoResultMessage()
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                ProcessId = message.ProcessId,
                Pipelines = pipelinesDTOs
            };

            _getPipelinesFromRepoResultProducer.PublishMessage(resultMessage);

            _logger.LogInformation("GetPipelinesFromRepoResultMessage Enqueued");

            return;
        }
    }
}
