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

namespace DAPM.Orchestrator.Processes
{
    public class DeletePipelineProcess : OrchestratorProcess
    {

        private Guid _organizationId;
        private Guid _repositoryId;
        private Guid _pipelineId;
        private Guid _ticketId;

        public DeletePipelineProcess(OrchestratorEngine engine, IServiceProvider serviceProvider, Guid ticketId, Guid processId,
            Guid organizationId, Guid repositoryId, Guid pipelineId)
            : base(engine, serviceProvider, processId)
        {
            _organizationId = organizationId;
            _repositoryId = repositoryId;
            _pipelineId = pipelineId;
            _ticketId = ticketId;
        }   
        public override void StartProcess()
        {
            var deletePipelineProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<DeletePipelineFromRepositoryMessage>>();

            var message = new DeletePipelineFromRepositoryMessage()
            {
                MessageId = Guid.NewGuid(),
                ProcessId = _processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                OrganizationId = _organizationId,
                RepositoryId = _repositoryId,
                PipelineId = _pipelineId,
            };

            deletePipelineProducer.PublishMessage(message);
        }

        public override void OnDeletePipelineFromRepositoryResult(DeletePipelineFromRepositoryResultMessage  message)
        {
            var deletePipelineProcessResultProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<DeletePipelineProcessResult>>();

            var processResultMessage = new DeletePipelineProcessResult()
            {
                TicketId = _ticketId,
                TimeToLive = TimeSpan.FromMinutes(1),
                WasDeleted = message.WasDeleted,
                MessageId = message.MessageId,
                Message = message.Message
            };

            deletePipelineProcessResultProducer.PublishMessage(processResultMessage);
            EndProcess();
        }
    }
}
