using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using CodeChallanger.UI.Configurations;
using System.Text;
using CodeExecutionContracts.Models;
using Newtonsoft.Json;
using CodeChallanger.UI.Interfaces;
using CodeChallanger.UI.Controllers;
using CodeChallenge.Domain.Interfaces;

namespace CodeChallanger.UI.Services
{
    public class RabbitMQConsumerService : BackgroundService
    {
        private readonly string _queueName;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly IConnectionFactory _connectionFactory;
        private readonly IOptions<RabbitMQOptions> options;

        private readonly ICacheService _cacheService;

        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMQConsumerService(
            IConnectionFactory connectionFactory, 
            IOptions<RabbitMQOptions> options, 
            IServiceScopeFactory scopeFactory
            )
        {

            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = options.Value.ConsumerQueueName;
            this.options = options;
            _scopeFactory = scopeFactory;
        }

     
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //ToDo: IBus service, Handle message
                CompilationResult compilationResult = JsonConvert.DeserializeObject<CompilationResult>(message);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var challengeServices = scope.ServiceProvider.GetRequiredService<ChallengeServices>();
                    challengeServices.SetCompilationResult(compilationResult);
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            await Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }

}
