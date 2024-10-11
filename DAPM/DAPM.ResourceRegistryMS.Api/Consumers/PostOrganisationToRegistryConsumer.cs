using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using RabbitMQLibrary.Messages.ResourceRegistry;

namespace DAPM.ResourceRegistryMS.Api.Consumers
{
    public class PostOrganisationToRegistryConsumer : IQueueConsumer<PostOrganisationToRegistryMessage>
    {
        private ILogger<PostOrganisationToRegistryConsumer> _logger;
        private IResourceService _resourceService;
        private IQueueProducer<PostOrganisationToRegistryResultMessage> _postOrganisationToRegistryResultProducer;
        public PostOrganisationToRegistryConsumer(ILogger<PostOrganisationToRegistryConsumer> logger,
            IQueueProducer<PostOrganisationToRegistryResultMessage> postOrganisationToRegistryResultProducer,
            IResourceService organisationService)
        {
            _logger = logger;
            _postOrganisationToRegistryResultProducer = postOrganisationToRegistryResultProducer;
            _organisationService = organisationService;
        }
        public async Task ConsumeAsync(PostOrganisationToRegistryMessage message)
        {
            _logger.LogInformation("PostOrganisationToRegistryMessage received");

            var OrganisationDto = message.Organisation;
            if (OrganisationDto != null)
            {
                var createdOrganisation = _organisationService.AddOrganisation(OrganisationDto);
                if(createdOrganisation != null)
                {
                    var resultMessage = new PostOrganisationToRegistryResultMessage
                    {
                        ProcessId = message.ProcessId,
                        TimeToLive = TimeSpan.FromMinutes(1),
                        Message = "Item created successfully",
                        Succeeded = true,
                        Organisation = OrganisationDto
                    };

                    _postOrganisationToRegistryResultProducer.PublishMessage(resultMessage);
                    _logger.LogInformation("PostOrganisationToRegistryResultMessage published");
                }
            }

            return;
        }
    }
}
