﻿using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Messages.Repository;
using RabbitMQLibrary.Messages.ResourceRegistry;
using RabbitMQLibrary.Models;
using System.IO;
using System.Xml.Linq;
using DAPM.ClientApi.LoggingExtensions;

/**
 * All new changes are made by:
 * @Author: s204197, s204423, s205339 s204452
 */
 
namespace DAPM.ClientApi.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ILogger<RepositoryService> _logger;
        private readonly ITicketService _ticketService;
        IQueueProducer<GetRepositoriesRequest> _getRepositoriesRequestProducer;
        IQueueProducer<GetResourcesRequest> _getResourcesRequestProducer;
        IQueueProducer<PostResourceRequest> _postResourceRequestProducer;
        IQueueProducer<PostOperatorRequest> _postOperatorRequestProducer;
        IQueueProducer<PostPipelineRequest> _postPipelineRequestProducer;
        IQueueProducer<GetPipelinesRequest> _getPipelinesRequestProducer;
        IQueueProducer<DeleteRepositoryByIdRequest> _deleteRepositoryRequestProducer;

        public RepositoryService(
            ILogger<RepositoryService> logger,
            ITicketService ticketService,
            IQueueProducer<GetRepositoriesRequest> getRepositoriesRequestProducer,
            IQueueProducer<GetResourcesRequest> getResourcesRequestProducer,
            IQueueProducer<PostResourceRequest> postResourceRequestProducer,
            IQueueProducer<PostPipelineRequest> postPipelineRequestProducer,
            IQueueProducer<GetPipelinesRequest> getPipelinesRequestProducer,
            IQueueProducer<PostOperatorRequest> postOperatorRequestProducer,
            IQueueProducer<DeleteRepositoryByIdRequest> deleteRepositoryRequestProducer) 
        {
            _ticketService = ticketService;
            _logger = logger;
            _getRepositoriesRequestProducer = getRepositoriesRequestProducer;
            _getResourcesRequestProducer = getResourcesRequestProducer;
            _postResourceRequestProducer = postResourceRequestProducer;
            _postPipelineRequestProducer = postPipelineRequestProducer;
            _getPipelinesRequestProducer = getPipelinesRequestProducer;
            _postOperatorRequestProducer = postOperatorRequestProducer;
            _deleteRepositoryRequestProducer = deleteRepositoryRequestProducer;
        }

        public Guid GetRepositoryById(Guid organizationId, Guid repositoryId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetRepositoriesRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = repositoryId
            };

            _getRepositoriesRequestProducer.PublishMessage(message);

            _logger.RepositoryGetRepositoriesEnqueued();

            return ticketId;
        }

        public Guid GetResourcesOfRepository(Guid organizationId, Guid repositoryId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetResourcesRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = repositoryId
            };

            _getResourcesRequestProducer.PublishMessage(message);

            _logger.RepositoryGetResourcesEnqueued();

            return ticketId;
        }
        // pineapple
        public Guid GetPipelinesOfRepository(Guid organizationId, Guid repositoryId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetPipelinesRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = repositoryId,
                PipelineId = null
            };
            
            _getPipelinesRequestProducer.PublishMessage(message);

            _logger.RepositoryGetPipelineEnqueued();

            return ticketId;
        }

        public Guid PostPipelineToRepository(Guid organizationId, Guid repositoryId, PipelineApiDto pipeline)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new PostPipelineRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = repositoryId,
                Name = pipeline.Name,
                Pipeline = pipeline.Pipeline,

            };

            _postPipelineRequestProducer.PublishMessage(message);

            _logger.RepositoryPostPipelineEnqueued();


            return ticketId;
        }

        public Guid PostResourceToRepository(Guid organizationId, Guid repositoryId, string name, IFormFile resourceFile, string resourceType)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            MemoryStream stream = new MemoryStream();
            resourceFile.CopyTo(stream);

            var fileDTO = new FileDTO()
            {
                Name = Path.GetFileNameWithoutExtension(resourceFile.FileName),
                Extension = Path.GetExtension(resourceFile.FileName),
                Content = stream.ToArray()
            };

            var message = new PostResourceRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = repositoryId,
                Name = name,
                ResourceType = resourceType,
                File = fileDTO,

            };

            _postResourceRequestProducer.PublishMessage(message);

            _logger.RepositoryPostResourceEnqueued();

            return ticketId;
        }

        public Guid PostOperatorToRepository(Guid organizationId, Guid repositoryId, string name, IFormFile sourceCodeFile, IFormFile dockerfileFile, string resourceType)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            MemoryStream sourceCodeStream = new MemoryStream();
            MemoryStream dockerFileStream = new MemoryStream();
            sourceCodeFile.CopyTo(sourceCodeStream);
            dockerfileFile.CopyTo(dockerFileStream);

            var sourceCodeFileDTO = new FileDTO()
            {
                Name = Path.GetFileNameWithoutExtension(sourceCodeFile.FileName),
                Extension = Path.GetExtension(sourceCodeFile.FileName),
                Content = sourceCodeStream.ToArray()
            };

            var dockerfileFileDTO = new FileDTO()
            {
                Name = Path.GetFileNameWithoutExtension(dockerfileFile.FileName),
                Extension = Path.GetExtension(dockerfileFile.FileName),
                Content = dockerFileStream.ToArray()
            };

            var message = new PostOperatorRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = repositoryId,
                Name = name,
                ResourceType = resourceType,
                SourceCodeFile = sourceCodeFileDTO,
                DockerfileFile = dockerfileFileDTO,
            };

            _postOperatorRequestProducer.PublishMessage(message);
            
            // TODO Fix
            _logger.RepositoryPostResourceEnqueued(); //Isn't this wrong? Should perhaps be operator

            return ticketId;
        }
        public Guid DeleteRepositoryById(Guid organizationId, Guid repositoryId)
        {
            Guid ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new DeleteRepositoryByIdRequest
            {
                MessageId = Guid.NewGuid(),
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = repositoryId
            };

            _deleteRepositoryRequestProducer.PublishMessage(message);

            _logger.LogDebug("DeleteRepositoryByIdRequest Enqueued");

            return ticketId;
        }
    }
}
