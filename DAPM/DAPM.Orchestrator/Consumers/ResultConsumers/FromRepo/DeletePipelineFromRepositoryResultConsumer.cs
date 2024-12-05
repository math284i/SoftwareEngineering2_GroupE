using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;

namespace DAPM.Orchestrator.Consumers.ResultConsumers.FromRepo
{
    public class DeletePipelineFromRepositoryResultConsumer : IQueueConsumer<DeletePipelineFromRepositoryResultMessage>
    {
        private IOrchestratorEngine _orchestratorEngine;

        public DeletePipelineFromRepositoryResultConsumer(IOrchestratorEngine orchestratorEngine)
        {
            _orchestratorEngine = orchestratorEngine;
        }

        public Task ConsumeAsync(DeletePipelineFromRepositoryResultMessage message)
        {
            OrchestratorProcess process = _orchestratorEngine.GetProcess(message.ProcessId);
            process.OnDeletePipelineFromRepositoryResult(message);

            return Task.CompletedTask;
        }
    }
}
