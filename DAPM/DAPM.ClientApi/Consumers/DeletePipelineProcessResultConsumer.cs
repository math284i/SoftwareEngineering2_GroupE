using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromRepo;

namespace DAPM.ClientApi.Consumers
{
    public class DeletePipelineProcessResultConsumer : IQueueConsumer<DeletePipelineProcessResult>
    {
        private ILogger<DeletePipelineProcessResultConsumer> _logger;
        private ITicketService _ticketService;

        public DeletePipelineProcessResultConsumer(ILogger<DeletePipelineProcessResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(DeletePipelineProcessResult message)
        {
            _logger.LogInformation("DeletePipelineProcessResult received");

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