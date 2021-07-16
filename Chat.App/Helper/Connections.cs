using RabbitMQ.Client;
namespace Chat.Client.Helper
{
    class Connections
    {
        public ConnectionFactory RabbitMQ { get; }      
        public Connections()
        {
            RabbitMQ = new ConnectionFactory() { HostName = "localhost" };
        }
    }
}
