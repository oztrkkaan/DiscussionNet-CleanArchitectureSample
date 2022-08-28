using RabbitMQ.Client;
using System.Text;

namespace Eskisehirspor.Infrastructure.RabbitMQ
{
    public class Publisher
    {
        private readonly RabbitMQService _rabbitMQService;
        public Publisher()
        {
            _rabbitMQService = new RabbitMQService();
        }
        public void PublishMessage(string queueName, string message)
        {
            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);
                    channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));
                }
            }
        }
    }
}
