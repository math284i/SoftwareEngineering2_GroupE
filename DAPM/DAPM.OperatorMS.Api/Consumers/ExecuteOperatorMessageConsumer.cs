using DAPM.OperatorMS.Api.LoggerExtensions;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Operator;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromOperator;

/**
 * All new changes are made by:
 * @Author: s204423, s205339 s204452
 */

namespace DAPM.OperatorMS.Api.Consumers
{
    public class ExecuteOperatorMessageConsumer : IQueueConsumer<ExecuteOperatorMessage>
    {
        private ILogger<ExecuteOperatorMessageConsumer> _logger;
        private IOperatorEngine _operatorEngine;
        private IServiceProvider _serviceProvider;
        protected IServiceScope _serviceScope;

        public ExecuteOperatorMessageConsumer(ILogger<ExecuteOperatorMessageConsumer> logger, IOperatorEngine operatorEngine, IServiceProvider serviceProvide) 
        {
            _logger = logger;
            _operatorEngine = operatorEngine;
            _serviceProvider = serviceProvide;
            _serviceScope = _serviceProvider.CreateScope();
        }

        public async Task ConsumeAsync(ExecuteOperatorMessage message) 
        {
            _logger.ExecuteMessageReceived();

            var succeeded = await _operatorEngine.StartOperatorExecution(message);

            // Publish ExecuteOperatorResultMessage to Orchestrator
            var executeOperatorResultMessageProducer = _serviceScope.ServiceProvider.GetRequiredService<IQueueProducer<ExecuteOperatorResultMessage>>();

            var executeOperatorResultMessage = new ExecuteOperatorResultMessage
            {
                ProcessId = message.ProcessId,
                TimeToLive = TimeSpan.FromMinutes(1),
                Succeeded = succeeded,
            };

            executeOperatorResultMessageProducer.PublishMessage(executeOperatorResultMessage);

            _operatorEngine.DeleteExecution(message.ProcessId);
        }
    }
}
