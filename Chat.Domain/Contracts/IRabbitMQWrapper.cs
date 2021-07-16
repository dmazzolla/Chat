using RabbitMQ.Client.Events;
using System;

namespace Chat.Domain.Contracts
{
    public interface IRabbitMQWrapper : IDisposable
    {
        EventingBasicConsumer Listen(string userName);

        void Submit(ChatMessage chat);
    }
}
