namespace CodeCompilator.Service.Configurations
{
    public class RabbitMQOptions
    {
        public string ConsumerQueueName { get; set; }
        public string ProduserQueueName { get; set; }
        public string HostName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
