using Chat.Domain.Contracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Linq;
using System.Text;

namespace Chat.Infraestructure
{
    public class RabbitMQWrapper : IRabbitMQWrapper
    {
        private readonly IConnection connection;
        private readonly IModel _channel;
        private readonly IRabbitMQConn _connRmq;

        public RabbitMQWrapper(IRabbitMQConn rmqConn)
        {
            _connRmq = rmqConn;
            connection = _connRmq.Conn.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void Dispose()
        {
            _channel.Dispose();
            connection.Dispose();
        }

        public EventingBasicConsumer Listen(string userName)
        {
            _channel.QueueDeclare(queue: userName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: userName, autoAck: true, consumer: consumer);
            return consumer;
        }

        public void Submit(Domain.ChatMessage chat)
        {
            string message = JsonConvert.SerializeObject(chat);
            byte[] body = Encoding.UTF8.GetBytes(message);

            if (chat.IsPublic)
            {
                foreach (var item in chat.Members.Where(x => x.NotEquals(chat.From)))
                {
                    _channel.BasicPublish(                        
                        exchange: "",
                        routingKey: item.NickName,
                        basicProperties: null,
                        body: body);
                }
            }
            else
            {
                _channel.BasicPublish(
                    exchange: "",
                    routingKey: chat.To.NickName,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
