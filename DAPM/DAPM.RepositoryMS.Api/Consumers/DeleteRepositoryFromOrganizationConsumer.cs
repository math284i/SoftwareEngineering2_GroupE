using DAPM.RepositoryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Models;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.RepositoryMS.Api.Consumers
{
    public class DeleteRepositoryFromOrganizationConsumer : IQueueConsumer<DeleteRepositoryFromOrganizationMessage>
{
        private readonly IRepositoryService _repositoryService;
        private readonly ILogger<DeleteRepositoryFromOrganizationConsumer> _logger;
        private readonly IQueueProducer<DeleteRepositoryFromOrganizationResultMessage> _resultMessageProducer;

    public DeleteRepositoryFromOrganizationConsumer(
            IRepositoryService repositoryService,
            ILogger<DeleteRepositoryFromOrganizationConsumer> logger,
            IQueueProducer<DeleteRepositoryFromOrganizationResultMessage> resultMessageProducer)
        {
            _repositoryService = repositoryService;
            _logger = logger;
            _resultMessageProducer = resultMessageProducer;
        }

        public async Task ConsumeAsync(DeleteRepositoryFromOrganizationMessage message)
        {
            _logger.LogInformation($"Received request to delete repository with ID {message.RepositoryId} for organization {message.OrganizationId}");

            bool result = await _repositoryService.DeleteRepository(message.OrganizationId, message.RepositoryId);

            if (result)
            {
                _logger.LogInformation($"Successfully deleted repository with ID {message.RepositoryId}");
            }
            else
            {
                _logger.LogWarning($"Failed to delete repository with ID {message.RepositoryId}");
            }

            var resultMessage = new DeleteRepositoryFromOrganizationResultMessage()
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                ProcessId = message.ProcessId, 
                WasDeleted = result,
                MessageId = message.MessageId,
                Message = result ? "Repository deleted successfully." : "Failed to delete repository."
            };

            _resultMessageProducer.PublishMessage(resultMessage);

            _logger.LogInformation("DeleteRepositoryFromOrganizationResultMessage Enqueued");
        }
    }
}
