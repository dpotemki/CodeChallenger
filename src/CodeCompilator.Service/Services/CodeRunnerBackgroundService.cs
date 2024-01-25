using CodeCompilator.Service.Configurations;
using CodeCompilator.Service.Interfaces;
using CodeExecutionContracts.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CodeCompilator.Service.Services
{
    public class CodeRunnerBackgroundService : BackgroundService
    {
        private readonly ICodeTestingService _codeTestingService;
        private readonly RabbitMQOptions _rabbitMQOptions;
        private readonly ChannelsConfiguration _channelsConfiguration;
        private readonly RabbitMQ.Client.IConnectionFactory _connectionFactory;

        public CodeRunnerBackgroundService(
            IOptions<RabbitMQOptions> rabbitMQOptions,
            IOptions<ChannelsConfiguration> channelsConfiguration,
            RabbitMQ.Client.IConnectionFactory connectionFactory,
            ICodeTestingService codeTestingService

            )
        {
            _rabbitMQOptions = rabbitMQOptions.Value;
            _channelsConfiguration = channelsConfiguration.Value;
            _connectionFactory = connectionFactory;
            _codeTestingService = codeTestingService;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _rabbitMQOptions.ConsumerQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            var messageChannel = Channel.CreateBounded<ExecuteCodeRequest>(new BoundedChannelOptions(_channelsConfiguration.MaxMessagesInChannel)
            {
                FullMode = BoundedChannelFullMode.Wait // Ожидание, когда в канале освободится место
            });

            consumer.Received += async (model, ea) =>
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return; // Прекращаем получение новых сообщений
                }

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var executeCodeRequest = JsonConvert.DeserializeObject<ExecuteCodeRequest>(message);

                await messageChannel.Writer.WriteAsync(executeCodeRequest, stoppingToken);
            };

            channel.BasicConsume(queue: _rabbitMQOptions.ConsumerQueueName, autoAck: true, consumer: consumer);

            var processingTasks = new Task[_channelsConfiguration.MaxParallelTasks];
            for (int i = 0; i < _channelsConfiguration.MaxParallelTasks; i++)
            {
                processingTasks[i] = ProcessMessagesAsync(messageChannel.Reader, stoppingToken);
            }

            await Task.WhenAll(processingTasks);

            channel.Close();
            connection.Close();
        }




        private async Task ProcessMessagesAsync(ChannelReader<ExecuteCodeRequest> reader, CancellationToken stoppingToken)
        {
            try
            {
                await foreach (var message in reader.ReadAllAsync(stoppingToken))
                {
                    // Обработка каждого сообщения
                    var result = await _codeTestingService.TestAndEvaluateCode(message, CancellationToken.None);
                    ProductResult(result);
                }
            }
            catch (OperationCanceledException)
            {
                // Операция была отменена, обработайте оставшиеся сообщения в канале
                while (reader.TryRead(out var message))
                {
                    var result = await _codeTestingService.TestAndEvaluateCode(message, CancellationToken.None);

                    ProductResult(result);

                }
            }
        }

        private void ProductResult(CompilationResult result)
        {
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            var resultJson = JsonConvert.SerializeObject(result);
            var resultBody = Encoding.UTF8.GetBytes(resultJson);
            channel.BasicPublish(exchange: "", routingKey: _rabbitMQOptions.ProduserQueueName, basicProperties: null, body: resultBody);
        }
    }
}
