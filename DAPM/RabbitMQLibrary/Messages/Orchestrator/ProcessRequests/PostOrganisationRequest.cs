using RabbitMQLibrary.Interfaces;

namespace RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;

public class PostOrganisationRequest : IQueueMessage
{
    public Guid MessageId { get; set; }
    public Guid TicketId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public string Name { get; set; }
    public Guid OrganizationId { get; set; }
}
