using RabbitMQ.Client;

namespace Chat.Domain.Contracts
{
    public interface IRabbitMQConn
    {
        ConnectionFactory Conn { get; }
    }
}
