using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DiscussionNet.Infrastructure.RabbitMQ
{
    public class Consumer
    {
        private readonly RabbitMQService _rabbitMQService;
        private string _queueName;
        public Consumer(string queueName)
        {
            _rabbitMQService = new RabbitMQService();
            _queueName = queueName;
        }
    }
}
