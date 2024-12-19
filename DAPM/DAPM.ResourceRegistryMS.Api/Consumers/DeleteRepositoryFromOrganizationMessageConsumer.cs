using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using Microsoft.Extensions.Logging;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.ResourceRegistryMS.Api.Consumers
{
    public class DeleteRepositoryFromOrganizationMessageConsumer : IQueueConsumer<DeleteRepositoryFromOrganizationMessage>
    {
        private ILogger<DeleteRepositoryFromOrganizationMessageConsumer> _logger;
        private IQueueProducer<DeleteRepositoryFromOrganizationResultMessage> _resultProducer;
        private IRepositoryService _repositoryService;
 
        public DeleteRepositoryFromOrganizationMessageConsumer(
            ILogger<DeleteRepositoryFromOrganizationMessageConsumer> logger,
            IQueueProducer<DeleteRepositoryFromOrganizationResultMessage> resultProducer,
            IRepositoryService repositoryService)
        {
            _logger = logger;
            _resultProducer = resultProducer;
            _repositoryService = repositoryService;
        }

        public async Task ConsumeAsync(DeleteRepositoryFromOrganizationMessage message)
        {
            _logger.LogInformation("DeleteRepositoriesMessage received");

            bool wasDeleted = false;
            string resultMessage = string.Empty;

            if (message.RepositoryId != null)
            {
                wasDeleted = await _repositoryService.DeleteRepositoryById(message.OrganizationId, (Guid)message.RepositoryId);
                resultMessage = wasDeleted ? "Repository deleted successfully." : "Failed to delete repository.";
            }
            else
            {
                // Handle deletion of all repositories in the organization if needed
                resultMessage = "RepositoryId is required for deletion.";
            }

            var responseMessage = new DeleteRepositoryFromOrganizationResultMessage
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
