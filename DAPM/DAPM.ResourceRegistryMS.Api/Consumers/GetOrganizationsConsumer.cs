﻿using DAPM.ResourceRegistryMS.Api.Models;
using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRegistry;
using RabbitMQLibrary.Messages.ResourceRegistry;
using RabbitMQLibrary.Models;
using System.Linq;
using DAPM.ResourceRegistryMS.Api.LoggingExtensions;
/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */
namespace DAPM.ResourceRegistryMS.Api.Consumers
{
    public class GetOrganizationsConsumer : IQueueConsumer<GetOrganizationsMessage>
    {
        private ILogger<GetOrganizationsConsumer> _logger;
        private IQueueProducer<GetOrganizationsResultMessage> _getOrganisationsResultQueueProducer;
        private IPeerService _peerService;

        public GetOrganizationsConsumer(
            ILogger<GetOrganizationsConsumer> logger, 
            IQueueProducer<GetOrganizationsResultMessage> getOrganisationsResultQueueProducer,
            IPeerService peerService)
        {
            _logger = logger;
            _getOrganisationsResultQueueProducer = getOrganisationsResultQueueProducer;
            _peerService = peerService;
        }

        public async Task ConsumeAsync(GetOrganizationsMessage message)
        {
            _logger.GetOrganizationMessageReceived();

            var peers = Enumerable.Empty<Peer>();

            if (message.OrganizationId != null)
            {
                var peer = await _peerService.GetPeer((Guid)message.OrganizationId);
                peers = peers.Append(peer);
            }
            else
            {
                peers = await _peerService.GetAllPeers();
            }


            IEnumerable<OrganizationDTO> organizations = Enumerable.Empty<OrganizationDTO>();

            foreach (var peer in peers)
            {
                var org = new OrganizationDTO
                {
                    Id = peer.Id,
                    Name = peer.Name,
                    Domain = peer.Domain,
                };

                organizations = organizations.Append(org);
            }

            var resultMessage = new GetOrganizationsResultMessage
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                ProcessId = message.ProcessId,
                Organizations = organizations
            };

            _getOrganisationsResultQueueProducer.PublishMessage(resultMessage);

            return;
        }

    }
}
