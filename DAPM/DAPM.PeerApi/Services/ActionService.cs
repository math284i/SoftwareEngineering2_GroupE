using DAPM.PeerApi.Services.Interfaces;
using DAPM.PipelineOrchestratorMS.Api.Models;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.Other;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DAPM.PeerApi.Services
{
    public class ActionService : IActionService
    {
        private readonly IQueueProducer<ExecuteOperatorActionRequest> _executeOperatorRequestProducer;
        private readonly IQueueProducer<TransferDataActionRequest> _transferDataRequestProducer;
        private readonly IQueueProducer<ActionResultReceivedMessage> _actionResultReceivedMessageProducer;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ActionService(
            IQueueProducer<ExecuteOperatorActionRequest> executeOperatorRequestProducer,
            IQueueProducer<TransferDataActionRequest> transferDataRequestProducer,
            IQueueProducer<ActionResultReceivedMessage> actionResultReceivedMessageProducer,
            IConfiguration configuration,
            HttpClient httpClient)
        {
            _executeOperatorRequestProducer = executeOperatorRequestProducer;
            _transferDataRequestProducer = transferDataRequestProducer;
            _actionResultReceivedMessageProducer = actionResultReceivedMessageProducer;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public void OnActionResultReceived(Guid processId, ActionResultDTO actionResult)
        {
            var message = new ActionResultReceivedMessage()
            {
                ExecutionId = actionResult.ExecutionId,
                StepId = actionResult.StepId,
                ProcessId = processId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Succeeded = true,
            };

            _actionResultReceivedMessageProducer.PublishMessage(message);
        }

        public void OnExecuteOperatorActionReceived(Guid senderProcessId, IdentityDTO senderIdentity, Guid stepId, ExecuteOperatorActionDTO data)
        {
            var message = new ExecuteOperatorActionRequest()
            {
                SenderProcessId = senderProcessId,
                TimeToLive = TimeSpan.FromMinutes(1),
                OrchestratorIdentity = senderIdentity,
                Data = data
            };

            _executeOperatorRequestProducer.PublishMessage(message);
        }

        public void OnTransferDataActionReceived(Guid senderProcessId, IdentityDTO senderIdentity, Guid stepId, TransferDataActionDTO data)
        {
            var message = new TransferDataActionRequest()
            {
                SenderProcessId = senderProcessId,
                TimeToLive = TimeSpan.FromMinutes(1),
                OrchestratorIdentity = senderIdentity,
                Data = data
            };

            _transferDataRequestProducer.PublishMessage(message);
        }

        //Mathias A
        public string GetAccessToken()
        {
            var tokenEndpoint = _configuration["Auth:TokenEndpoint"];
            var clientId = _configuration["Auth:ClientId"];
            var clientSecret = _configuration["Auth:ClientSecret"];

            var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials",
                    ["client_id"] = clientId,
                    ["client_secret"] = clientSecret
                })
            };

            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var token = JsonSerializer.Deserialize<TokenResponse>(content);

            return token.AccessToken;
        }

        public IEnumerable<string> GetUserRoles(string userId)
        {
            var accessToken = GetAccessToken();
            var userRolesEndpoint = _configuration["Auth:UserRolesEndpoint"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = _httpClient.GetAsync($"{userRolesEndpoint}/{userId}").Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var roles = JsonSerializer.Deserialize<string[]>(content);

            return roles;
        }

        public bool IsAuthorized(string userId)
        {
            var roles = GetUserRoles(userId);
            // eventuel logic for rolle valg? 
            // Eksempel herefter:
            return roles.Contains("Admin") || roles.Contains("User");
        }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
    }
}