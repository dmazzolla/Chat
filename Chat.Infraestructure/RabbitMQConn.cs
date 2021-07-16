using Chat.Domain.Contracts;
using RabbitMQ.Client;
using System.Configuration;

namespace Chat.Infraestructure
{
    public class RabbitMQConn : IRabbitMQConn
    {
        public ConnectionFactory Conn { get; }
        public RabbitMQConn()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = ConfigurationManager.AppSettings["RabbitMQConn"], 
                VirtualHost = ConfigurationManager.AppSettings["RabbitMQVHost"]
            };
            Conn = connectionFactory;
        }
    }
}