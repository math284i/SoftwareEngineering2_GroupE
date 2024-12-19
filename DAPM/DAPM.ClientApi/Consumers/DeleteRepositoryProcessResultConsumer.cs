using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;

/**
 * All new changes are made by:
 * @Author: s204197, s204423, s205339 s204452
 */
 
namespace DAPM.ClientApi.Consumers
{
    public class DeleteRepositoryProcessResultConsumer : IQueueConsumer<DeleteRepositoryProcessResult>
    {
        private ILogger<DeleteRepositoryProcessResultConsumer> _logger;
        private ITicketService _ticketService;

        public DeleteRepositoryProcessResultConsumer(ILogger<DeleteRepositoryProcessResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(DeleteRepositoryProcessResult message)
        {
            _logger.LogInformation("DeleteRepositoryProcessResult received");

            JObject result = new JObject
            {
                ["wasDeleted"] = message.WasDeleted,
                ["message"] = message.Message
            };

            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
