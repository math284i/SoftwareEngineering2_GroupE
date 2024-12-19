using DAPM.ClientApi.LoggingExtensions;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Interfaces;

/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */

namespace DAPM.ClientApi.Consumers
{
    public class PostPipelineCommandProcessResultConsumer : IQueueConsumer<PostPipelineCommandProcessResult>
    {
        private ILogger<PostPipelineCommandProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public PostPipelineCommandProcessResultConsumer(ILogger<PostPipelineCommandProcessResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(PostPipelineCommandProcessResult message)
        {
            _logger.PostPipelineRecieved();

            var executionIdString = "executionId";
            var succeededString = "succeeded";
            var messageString = "message";


            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            //Serialization
            result[executionIdString] = message.ExecutionId;
            result[succeededString] = message.Succeeded;
            result[messageString] = message.Message;

            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
