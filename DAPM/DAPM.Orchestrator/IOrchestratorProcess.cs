﻿using RabbitMQLibrary.Messages.Orchestrator.Other;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromOperator;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPeerApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPipelineOrchestrator;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;
using RabbitMQLibrary.Messages.PeerApi;
using RabbitMQLibrary.Messages.Repository;

/**
 * All new changes are made by:
 * @Author: s204197 and s216160
 */
 
namespace DAPM.Orchestrator
{
    public interface IOrchestratorProcess
    {
        public void StartProcess();
        public void EndProcess();
        public void OnPostRepoToRegistryResult(PostRepoToRegistryResultMessage message);
        public void OnAddResourceToRegistryResult();
        public void OnGetOrganizationsFromRegistryResult(GetOrganizationsResultMessage message);
        public void OnGetRepositoriesFromRegistryResult(GetRepositoriesResultMessage message);
        public void OnGetResourcesFromRegistryResult(GetResourcesResultMessage message);
        public void OnGetPipelinesFromRegistryResult(GetPipelinesResultMessage message);
        public void OnGetPipelinesFromRepoResult(GetPipelinesFromRepoResultMessage message);
        public void OnPostResourceToRepoResult(PostResourceToRepoResultMessage message);
        public void OnPostResourceToOperatorResult(PostInputResourceResultMessage message);
        public void OnPostResourceToRegistryResult(PostResourceToRegistryResultMessage message);
        public void OnPostPipelineToRepoResult(PostPipelineToRepoResultMessage message);
        public void OnPostPipelineToRegistryResult(PostPipelineToRegistryResultMessage message);
        public void OnCreateRepoInRepoResult(PostRepoToRepoResultMessage message);
        public void OnGetResourceFilesFromRepoResult(GetResourceFilesFromRepoResultMessage message);
        public void OnGetOperatorFilesFromRepoResult(GetOperatorFilesFromRepoResultMessage message);
        public void OnGetResourceFilesFromOperatorResult(GetExecutionOutputResultMessage message);
        public void OnSendResourceToPeerResult(SendResourceToPeerResultMessage message);
        public void OnActionResultFromPeer(ActionResultReceivedMessage message);
        public void OnDeleteRepositoryFromOrganizationResult(DeleteRepositoryFromOrganizationResultMessage message);

        public void OnHandshakeRequestResponse(HandshakeRequestResponseMessage message);
        public void OnRegistryUpdate(RegistryUpdateMessage message);
        public void OnApplyRegistryUpdateResult(ApplyRegistryUpdateResult message);
        public void OnGetEntriesFromOrgResult(GetEntriesFromOrgResult message);
        public void OnRegistryUpdateAck(RegistryUpdateAckMessage message);

        public void OnCreatePipelineExecutionResult(CreatePipelineExecutionResultMessage message);
        public void OnCommandEnqueued(CommandEnqueuedMessage message);
        public void OnExecuteOperatorResult(ExecuteOperatorResultMessage message);
        public void OnGetPipelineExecutionStatusResult(GetPipelineExecutionStatusResultMessage message);
        public void OnDeletePipelineFromRepositoryResult(DeletePipelineFromRepositoryResultMessage message);

    }
}
