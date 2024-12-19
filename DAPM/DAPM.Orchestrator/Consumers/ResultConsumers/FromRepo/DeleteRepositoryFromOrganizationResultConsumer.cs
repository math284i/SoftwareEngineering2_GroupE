using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;

/**
 * All new changes are made by:
 * @Author: s204197
 */
 
namespace DAPM.Orchestrator.Consumers.ResultConsumers.FromRepo
{
    public class DeleteRepositoryFromOrganizationResultConsumer : IQueueConsumer<DeleteRepositoryFromOrganizationResultMessage>
    {
        private IOrchestratorEngine _orchestratorEngine;

        public DeleteRepositoryFromOrganizationResultConsumer(IOrchestratorEngine orchestratorEngine)
        {
            _orchestratorEngine = orchestratorEngine;
        }

        public Task ConsumeAsync(DeleteRepositoryFromOrganizationResultMessage message)
        {
            OrchestratorProcess process = _orchestratorEngine.GetProcess(message.ProcessId);
            process.OnDeleteRepositoryFromOrganizationResult(message);

            return Task.CompletedTask;
        }
    }
}
