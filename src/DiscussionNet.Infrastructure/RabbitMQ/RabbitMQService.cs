using RabbitMQ.Client;

namespace DiscussionNet.Infrastructure.RabbitMQ
{
    public class RabbitMQService
    {
        private readonly string _hostName = "localhost";
        public IConnection GetRabbitMQConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _hostName
            };
            return connectionFactory.CreateConnection();
        }
    }
}
