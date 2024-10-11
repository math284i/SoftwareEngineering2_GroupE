using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ResourceRegistry;
using System.Threading.Tasks;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;

namespace DAPM.ResourceRegistryMS.Api.Consumers
{
    public class PostOrganisationToRegistryConsumer : IQueueConsumer<PostOrganisationToRegistryMessage>
    {
        private readonly ILogger<PostOrganisationToRegistryConsumer> _logger;
        private readonly IQueueProducer<PostOrganisationToRegistryResultMessage> _postOrganisationToRegistryResultProducer;
        private readonly IResourceService _resourceService;

        public PostOrganisationToRegistryConsumer(
            ILogger<PostOrganisationToRegistryConsumer> logger,
            IResourceService resourceService,
            IQueueProducer<PostOrganisationToRegistryResultMessage> postOrganisationToRegistryResultProducer)
        {
            _logger = logger;
            _resourceService = resourceService;
            _postOrganisationToRegistryResultProducer = postOrganisationToRegistryResultProducer;
        }

        public async Task ConsumeAsync(PostOrganisationToRegistryMessage message)
        {
            _logger.LogInformation($"Consuming message with ID: {message.MessageId}");

            // Process the message
            var result = await _resourceService.ProcessOrganisationAsync(message.Organization);

            // Create result message
            var resultMessage = new PostOrganisationToRegistryResultMessage
            {
                MessageId = message.MessageId,
                ProcessId = message.ProcessId,
                Success = result.Success,
                Errors = result.Errors
            };

            // Send result message to the queue
            await _postOrganisationToRegistryResultProducer.ProduceAsync(resultMessage);
        }
    }
}