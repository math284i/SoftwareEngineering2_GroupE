using DAPM.ClientApi.LoggingExtensions;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;

/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */

namespace DAPM.ClientApi.Services
{

    public enum TicketStatus
    {
        NotCompleted = 0,
        Completed = 1,
        Failed = 2,
        NotFound = 3,
    }

    public enum TicketResolutionType
    {
        Json = 0,
        File = 1,
    }

    public class TicketService : ITicketService
    {
        private readonly ILogger<TicketService> _logger;
        private Dictionary<Guid, JToken> _ticketResolutions;
        private Dictionary<Guid, TicketStatus> _ticketStatus;
        private Dictionary<Guid, TicketResolutionType> _ticketResolutionType;

        public TicketService(ILogger<TicketService> logger) 
        {
            _logger = logger;
            _ticketStatus = new Dictionary<Guid, TicketStatus>();
            _ticketResolutions = new Dictionary<Guid, JToken>();
            _ticketResolutionType = new Dictionary<Guid, TicketResolutionType>();
        }
        public JToken GetTicketResolution(Guid ticketId)
        {
            var ticketIdString = "ticketId";
            var statusString = "status";
            var messageString = "message";
            var resultString = "result";
            
            JToken resolution = new JObject();
            resolution[ticketIdString] = ticketId;

            if (_ticketStatus.ContainsKey(ticketId))
            {
                resolution[statusString] = (int)_ticketStatus[ticketId];

                switch (_ticketStatus[ticketId])
                {
                    case TicketStatus.NotCompleted:
                        resolution[messageString] = "The ticket hasn't been completed";
                        resolution[resultString] = null;
                        break;

                    case TicketStatus.Completed:
                        resolution[messageString] = "The ticket has been completed";
                        resolution[resultString] = _ticketResolutions[ticketId];
                        break;

                    case TicketStatus.Failed:
                        resolution[messageString] = "The ticket resolution failed";
                        resolution[resultString] = null;
                        break;
                }
            }
            else
            {
                resolution[statusString] = (int)TicketStatus.NotFound;
                resolution[messageString] = "The ticket does not exist";
                resolution[resultString] = null;
            }

            return resolution;
        }

        public void UpdateTicketStatus(Guid ticketId, TicketStatus ticketStatus)
        {
            if(_ticketStatus.ContainsKey(ticketId))
            {
                _ticketStatus[ticketId] = ticketStatus;
            }
            else
            {
                _logger.TicketStatusUpdated(ticketId);
                return;
            }
        }

        public TicketStatus GetTicketStatus(Guid ticketId)
        {
            if (_ticketResolutions.ContainsKey(ticketId))
            {
                return _ticketStatus[ticketId];
            }
            else
                return TicketStatus.NotFound;
        }

        public TicketResolutionType GetTicketResolutionType(Guid ticketId)
        {
            return _ticketResolutionType[ticketId];
        }

        public Guid CreateNewTicket(TicketResolutionType resolutionType)
        {
            Guid guid = Guid.NewGuid();
            _ticketStatus[guid] = TicketStatus.NotCompleted;
            _ticketResolutionType[guid] = resolutionType;
            _logger.TicketCreated();
            return guid;
        }

        public void UpdateTicketResolution(Guid ticketId, JToken requestResult)
        {
            if(_ticketStatus.ContainsKey(ticketId))
            {
                UpdateTicketStatus(ticketId, TicketStatus.Completed);
                _ticketResolutions[ticketId] = requestResult;
            }
            else
            {
                _logger.TicketResolutionUpdated(ticketId);
                return;
            }
            _logger.TicketResolution(ticketId);
        }
    }
}
