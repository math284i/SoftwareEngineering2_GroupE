using DAPM.RepositoryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Models;

/**
 * All new changes are made by:
 * @Author: s216160
 */

namespace DAPM.RepositoryMS.Api.Consumers
{
    public class DeletePipelineFromRepositoryConsumer : IQueueConsumer<DeletePipelineFromRepositoryMessage>
{
        private readonly IPipelineService _pipelineService;
        private readonly ILogger<DeletePipelineFromRepositoryConsumer> _logger;
        private readonly IQueueProducer<DeletePipelineFromRepositoryResultMessage> _resultMessageProducer;

    public DeletePipelineFromRepositoryConsumer(
            IPipelineService pipelineService,
            ILogger<DeletePipelineFromRepositoryConsumer> logger,
            IQueueProducer<DeletePipelineFromRepositoryResultMessage> resultMessageProducer)
        {
            _pipelineService = pipelineService;
            _logger = logger;
            _resultMessageProducer = resultMessageProducer;
        }

        public async Task ConsumeAsync(DeletePipelineFromRepositoryMessage message)
        {
            _logger.LogInformation($"Received request to delete pipeline with ID {message.PipelineId} for Repository {message.RepositoryId}");

            bool result = await _pipelineService.DeletePipeline(message.OrganizationId, message.RepositoryId, message.PipelineId);

            if (result)
            {
                _logger.LogInformation($"Successfully deleted pipeline with ID {message.PipelineId}");
            }
            else
            {
                _logger.LogWarning($"Failed to delete pipeline with ID {message.PipelineId}");
            }

            var resultMessage = new DeletePipelineFromRepositoryResultMessage()
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                ProcessId = message.ProcessId, 
                WasDeleted = result,
                MessageId = message.MessageId,
                Message = result ? "Pipeline deleted successfully." : "Failed to delete pipeline.",
            };

            _resultMessageProducer.PublishMessage(resultMessage);

            _logger.LogInformation("DeletePipelineFromRepositoryResultMessage Enqueued");
        }
    }
}
