using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * All new changes are made by:
 * @Author: s216160
 */

namespace RabbitMQLibrary.Messages.ClientApi
{
    public class DeletePipelineProcessResult : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public Guid TicketId { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public bool WasDeleted { get; set; }
        public string Message { get; set; } = "Message not set";
    }
}
