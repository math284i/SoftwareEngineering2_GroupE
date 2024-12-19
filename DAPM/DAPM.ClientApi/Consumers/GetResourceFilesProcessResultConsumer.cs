using DAPM.ClientApi.LoggingExtensions;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;
using RabbitMQLibrary.Interfaces;

/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */

namespace DAPM.ClientApi.Consumers
{
    public class GetResourceFilesProcessResultConsumer : IQueueConsumer<GetResourceFilesProcessResult>
    {
        private ILogger<GetResourceFilesProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public GetResourceFilesProcessResultConsumer(ILogger<GetResourceFilesProcessResultConsumer> logger,
            ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetResourceFilesProcessResult message)
        {
            _logger.GetResourceFilesReceived();
            
            _logger.FileName(message.Files.First().Name);
            
            IEnumerable<FileDTO> filesDTOs = message.Files;

            var filePathString = "filePath";
            var fileNameString = "fileName";
            var fileFormatString = "fileFormat";

            if(filesDTOs.Any())
            {
                var firstFile = filesDTOs.First();
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "TemporaryFiles");
                var filePath = Path.Combine(directoryPath, Path.GetRandomFileName());

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                File.WriteAllBytes(filePath, firstFile.Content);

                JToken result = new JObject();
                //Serialization
                result[filePathString] = filePath;
                result[fileNameString] = firstFile.Name;
                result[fileFormatString] = firstFile.Extension;


                // Update resolution
                _ticketService.UpdateTicketResolution(message.TicketId, result);

            }

            return Task.CompletedTask;
        }
    }
}
