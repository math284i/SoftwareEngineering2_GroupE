using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPeerApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.PeerApi;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Messages.ResourceRegistry;
using RabbitMQLibrary.Models;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.Orchestrator.Processes
{
    public class DeleteRepositoryProcess : OrchestratorProcess
    {

        private Guid _organizationId;
        private Guid _repositoryId;
        private Guid _ticketId;

        public DeleteRepositoryProcess(OrchestratorEngine engine, IServiceProvider serviceProvider, Guid ticketId, Guid processId,
            Guid organizationId, Guid repositoryId)
            : base(engine, serviceProvider, processId)
        {
            _organizationId = organizationId;
            _repositoryId = repositoryId;
            _ticketId = ticketId;
        }   
        public override void StartProcess()
        {
            var deleteRepositoryProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<DeleteRepositoryFromOrganizationMessage>>();

            var message = new DeleteRepositoryFromOrganizationMessage()
            {
                MessageId = Guid.NewGuid(),
                ProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                OrganizationId = _organizationId,
                RepositoryId = _repositoryId,
            };

            deleteRepositoryProducer.PublishMessage(message);
        }

        public override void OnDeleteRepositoryFromOrganizationResult(DeleteRepositoryFromOrganizationResultMessage  message)
        {
            var deleteRepositoryProcessResultProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<DeleteRepositoryProcessResult>>();

            var processResultMessage = new DeleteRepositoryProcessResult()
            {
                TicketId = _ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                WasDeleted = message.WasDeleted,
                MessageId = message.MessageId,
                Message = message.Message
            };

            deleteRepositoryProcessResultProducer.PublishMessage(processResultMessage);
            EndProcess();
        }
    }
}
