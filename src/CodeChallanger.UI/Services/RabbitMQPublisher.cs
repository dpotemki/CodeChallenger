using CodeChallanger.UI.Configurations;
using CodeChallenge.Domain.Interfaces.Models;
using CodeExecutionContracts.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CodeChallanger.UI.Services
{
    public class RabbitMQPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConnectionFactory _connectionFactory;

        public RabbitMQPublisher(IConnectionFactory connectionFactory,IOptions<RabbitMQOptions> options)
        {
            //ToDo IBus service 
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PublishExecuteCodeRequest(ExecuteCodeRequest request, string queueName)
        {
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonConvert.SerializeObject(request);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }
}
