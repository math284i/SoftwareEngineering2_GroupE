using DAPM.Orchestrator.Processes;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;

/**
 * All new changes are made by:
 * @Author: s216160
 */

namespace DAPM.Orchestrator.Consumers.StartProcessConsumers
{
    public class DeletePipelineByIdRequestConsumer : IQueueConsumer<DeletePipelineByIdRequest>
    {
        IOrchestratorEngine _engine;

        public DeletePipelineByIdRequestConsumer(IOrchestratorEngine engine)
        {
            _engine = engine;
        }

        public Task ConsumeAsync(DeletePipelineByIdRequest message)
        {
            _engine.StartDeletePipelineProcess(message.TicketId, message.OrganizationId, message.RepositoryId, message.PipelineId);
            return Task.CompletedTask;
        }
    }
}
