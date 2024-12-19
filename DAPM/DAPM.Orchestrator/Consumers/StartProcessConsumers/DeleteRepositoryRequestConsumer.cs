using DAPM.Orchestrator.Processes;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.Orchestrator.Consumers.StartProcessConsumers
{
    public class DeleteRepositoryByIdRequestConsumer : IQueueConsumer<DeleteRepositoryByIdRequest>
    {
        IOrchestratorEngine _engine;

        public DeleteRepositoryByIdRequestConsumer(IOrchestratorEngine engine)
        {
            _engine = engine;
        }

        public Task ConsumeAsync(DeleteRepositoryByIdRequest message)
        {
            _engine.StartDeleteRepositoryProcess(message.TicketId, message.OrganizationId, message.RepositoryId);
            return Task.CompletedTask;
        }
    }
}
