using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Repository;
using DAPM.Orchestrator.Processes;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;

namespace DAPM.Orchestrator.Consumers.ResultConsumers.FromRegistry
{
    public class DeleteRepositoryResultConsumer : IQueueConsumer<DeleteRepositoryFromOrganizationResultMessage>
    {
        private readonly IOrchestratorEngine _orchestratorEngine;

        public DeleteRepositoryResultConsumer(IOrchestratorEngine orchestratorEngine)
        {
            _orchestratorEngine = orchestratorEngine;
        }

        public Task ConsumeAsync(DeleteRepositoryFromOrganizationResultMessage message)
        {
            DeleteRepositoryProcess process = (DeleteRepositoryProcess)_orchestratorEngine.GetProcess(message.ProcessId);
            process.OnDeleteRepositoryFromOrganizationResult(message);

            return Task.CompletedTask;
        }
    }
}
