using DAPM.ResourceRegistryMS.Api.LoggingExtensions;
using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using RabbitMQLibrary.Messages.ResourceRegistry;

namespace DAPM.ResourceRegistryMS.Api.Consumers
{
    public class PostResourceToRegistryConsumer : IQueueConsumer<PostResourceToRegistryMessage>
    {
        private ILogger<PostResourceToRegistryConsumer> _logger;
        private IResourceService _resourceService;
        private IQueueProducer<PostResourceToRegistryResultMessage> _postResourceToRegistryResultProducer;
        public PostResourceToRegistryConsumer(ILogger<PostResourceToRegistryConsumer> logger,
            IQueueProducer<PostResourceToRegistryResultMessage> postResourceToRegistryResultProducer,
            IResourceService resourceService)
        {
            _logger = logger;
            _postResourceToRegistryResultProducer = postResourceToRegistryResultProducer;
            _resourceService = resourceService;
        }
        public async Task ConsumeAsync(PostResourceToRegistryMessage message)
        {
            _logger.PostResourceMessageReceived();

            var resourceDto = message.Resource;
            if (resourceDto == null) return;
            var createdResource = _resourceService.AddResource(resourceDto);
            if (createdResource == null) return;
            var resultMessage = new PostResourceToRegistryResultMessage
            {
                ProcessId = message.ProcessId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Message = "Item created successfully",
                Succeeded = true,
                Resource = resourceDto
            };

            _postResourceToRegistryResultProducer.PublishMessage(resultMessage);
            _logger.PostResourceMessagePublished();

            return;
        }
    }
}
