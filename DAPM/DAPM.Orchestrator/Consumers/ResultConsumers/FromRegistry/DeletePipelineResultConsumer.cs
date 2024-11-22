using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Repository;
using DAPM.Orchestrator.Processes;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;

namespace DAPM.Orchestrator.Consumers.ResultConsumers.FromRegistry
{
    public class DeletePipelineResultConsumer : IQueueConsumer<DeletePipelineFromRepositoryResultMessage>
    {
        private readonly IOrchestratorEngine _orchestratorEngine;

        public DeletePipelineResultConsumer(IOrchestratorEngine orchestratorEngine)
        {
            _orchestratorEngine = orchestratorEngine;
        }

        public Task ConsumeAsync(DeletePipelineFromRepositoryResultMessage message)
        {
            DeletePipelineProcess process = (DeletePipelineProcess)_orchestratorEngine.GetProcess(message.ProcessId);
            process.OnDeletePipelineFromRepositoryResult(message);

            return Task.CompletedTask;
        }
    }
}
