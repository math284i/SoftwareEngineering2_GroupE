using DAPM.ClientApi.LoggingExtensions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;
using DAPM.ClientApi.Services.Interfaces;

/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */

namespace DAPM.ClientApi.Consumers
{
    public class CollabHandshakeProcessResultConsumer : IQueueConsumer<CollabHandshakeProcessResult>
    {
        private ILogger<CollabHandshakeProcessResultConsumer> _logger;
        private ITicketService _ticketService;

        public CollabHandshakeProcessResultConsumer(ILogger<CollabHandshakeProcessResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(CollabHandshakeProcessResult message)
        {
            _logger.CollabReceived();
            var requestedString = "requestedPeerIdentity";
            var succeededString = "succeeded";
            var messageString = "message";
            
            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            JToken requestedPeerIdentityJson = JToken.FromObject(message.RequestedPeerIdentity, serializer);

            //Serialization
            result[requestedString] = requestedPeerIdentityJson;
            result[succeededString] = message.Succeeded;
            result[messageString] = message.Message;


            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
