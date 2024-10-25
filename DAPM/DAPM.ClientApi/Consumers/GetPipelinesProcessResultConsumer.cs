using DAPM.ClientApi.LoggingExtensions;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class GetPipelinesProcessResultConsumer : IQueueConsumer<GetPipelinesProcessResult>
    {
        private ILogger<GetPipelinesProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public GetPipelinesProcessResultConsumer(ILogger<GetPipelinesProcessResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetPipelinesProcessResult message)
        {
            _logger.GetPipelineProcessReceived();
            var pipelinesString = "pipelines";

            IEnumerable<PipelineDTO> pipelinesDTOs = message.Pipelines;

            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


            //Serialization
            JToken pipelinesJSON = JToken.FromObject(pipelinesDTOs, serializer);
            result[pipelinesString] = pipelinesJSON;


            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
