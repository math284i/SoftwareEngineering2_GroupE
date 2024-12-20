﻿using DAPM.ClientApi.LoggingExtensions;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Models;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults;
using RabbitMQLibrary.Messages.ClientApi;

/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */

namespace DAPM.ClientApi.Consumers
{
    public class GetResourcesProcessResultConsumer : IQueueConsumer<GetResourcesProcessResult>
    {
        private ILogger<GetResourcesProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public GetResourcesProcessResultConsumer(ILogger<GetResourcesProcessResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetResourcesProcessResult message)
        {
            _logger.GetRessourceProcessReceived();
            var ressourcesString = "resources";

            IEnumerable<ResourceDTO> resourcesDTOs = message.Resources;

            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


            //Serialization
            JToken resourcesJSON = JToken.FromObject(resourcesDTOs, serializer);
            result[ressourcesString] = resourcesJSON;


            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
