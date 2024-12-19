using DAPM.ClientApi.LoggingExtensions;
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
    public class GetRepositoriesProcessResultConsumer : IQueueConsumer<GetRepositoriesProcessResult>
    {
        private ILogger<GetRepositoriesProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public GetRepositoriesProcessResultConsumer(ILogger<GetRepositoriesProcessResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetRepositoriesProcessResult message)
        {
            _logger.GetRepositoriesReceived();
            var repositoriesString = "repositories";

            IEnumerable<RepositoryDTO> repositoriesDTOs = message.Repositories;

            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


            //Serialization
            JToken repositoriesJSON = JToken.FromObject(repositoriesDTOs, serializer);
            result[repositoriesString] = repositoriesJSON;


            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
