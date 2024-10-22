using DAPM.ClientApi.LoggingExtensions;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class PostItemResultConsumer : IQueueConsumer<PostItemProcessResult>
    {
        private ILogger<PostItemResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public PostItemResultConsumer(ILogger<PostItemResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(PostItemProcessResult message)
        {
            _logger.PostItemReceived();
            var itemIdsString = "itemIds";
            var itemTypeString = "itemType";
            var succeededString = "succeeded";
            var messageString = "message";

            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            JToken idsJSON = JToken.FromObject(message.ItemIds, serializer);

            //Serialization
            result[itemIdsString] = idsJSON;
            result[itemTypeString] = message.ItemType;
            result[succeededString] = message.Succeeded;
            result[messageString] = message.Message;  

            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
