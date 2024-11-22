using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using Microsoft.Extensions.Logging;

namespace DAPM.ResourceRegistryMS.Api.Consumers
{
    public class DeletePipelineFromRepositoryMessageConsumer : IQueueConsumer<DeletePipelineFromRepositoryMessage>
    {
        private ILogger<DeletePipelineFromRepositoryMessageConsumer> _logger;
        private IQueueProducer<DeletePipelineFromRepositoryResultMessage> _resultProducer;
        private IPipelineService _pipelineService;
 
        public DeletePipelineFromRepositoryMessageConsumer(
            ILogger<DeletePipelineFromRepositoryMessageConsumer> logger,
            IQueueProducer<DeletePipelineFromRepositoryResultMessage> resultProducer,
            IPipelineService pipelineService)
        {
            _logger = logger;
            _resultProducer = resultProducer;
            _pipelineService = pipelineService;
        }

        public async Task ConsumeAsync(DeletePipelineFromRepositoryMessage message)
        {
            _logger.LogInformation("DeletePipelineMessage received");

            bool wasDeleted = false;
            string resultMessage = string.Empty;

            if (message.PipelineId != null)
            {
                wasDeleted = await _pipelineService.DeletePipelineById(message.OrganizationId, (Guid)message.RepositoryId, (Guid)message.PipelineId);
                resultMessage = wasDeleted ? "Pipeline deleted successfully." : "Failed to delete pipeline.";
            }
            else
            {
                // Handle deletion of all repositories in the organization if needed
                resultMessage = "Pipeline is required for deletion.";
            }

            var responseMessage = new DeletePipelineFromRepositoryResultMessage
            {
                ProcessId = message.ProcessId,
                TimeToLive = TimeSpan.FromMinutes(1),
                WasDeleted = wasDeleted,
                Message = resultMessage
            };

            _resultProducer.PublishMessage(responseMessage);
        }
    }
}
