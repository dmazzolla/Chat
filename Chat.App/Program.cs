using Chat.Business.Services;
using Chat.Common;
using Chat.Common.Resources;
using Chat.Domain;
using Chat.Domain.Contracts;
using Chat.Infraestructure;
using Chat.Infraestructure.Helpers.RestSharp;
using Chat.Model;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                    .AddSingleton<IRabbitMQConn, RabbitMQConn>()
                    .AddSingleton<IRabbitMQWrapper, RabbitMQWrapper>()
                    .AddSingleton<IRestSharpWrapper, RestSharpWrapper>()
                    .AddSingleton<IChatService, ChatService>()
               .BuildServiceProvider();

            string msg = serviceProvider.GetService<IChatService>().CreateVhostChat();
            if (msg.IsNotNullOrEmptyOrWhiteSpace())
            {
                Console.WriteLine(msg);
                Environment.Exit(-1);
            }

            List<User> lstMembers = serviceProvider.GetService<IChatService>().GetUsers();
            Console.WriteLine(Messages.UsersChat);
            Console.WriteLine(string.Empty);
            foreach (var item in lstMembers)
            {
                Console.WriteLine(item.NickName);
            }
            
            Console.WriteLine(string.Empty);

            User user;
            while (true)
            {
                Console.WriteLine(Messages.EnterYourNickname);
                user = new User(Console.ReadLine());
                try
                {
                    user.Validate();
                    user.ValidateWithMembers(lstMembers);
                    break;
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(string.Empty);
                    continue;
                }
            }
            
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Format(Messages.UserWelcome, user.NickName));
            Console.WriteLine(string.Empty);
            Console.WriteLine(Messages.ExamplesSendMsg);
            Console.WriteLine(Messages.ExamplePrivateMsg);
            Console.WriteLine(Messages.ExamplePublicMsg);
            Console.WriteLine(string.Empty);

            var chat = serviceProvider.GetService<IRabbitMQWrapper>();

            chat.Listen(user.NickName).Received += Received;

            while (true)
            {                
                ChatMessage chatMessage = new ChatMessage(Console.ReadLine(), user.NickName, serviceProvider.GetService<IChatService>().GetUsers());
                try
                {
                    chatMessage.Validate();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(string.Empty);
                    continue;
                }

                chat.Submit(chatMessage);
                Console.WriteLine(string.Empty);
            }
        }

        private static void Received(object sender, BasicDeliverEventArgs e)
        {
            MsgChatModel msg = JsonConvert.DeserializeObject<MsgChatModel>(Encoding.UTF8.GetString(e.Body.ToArray()));
            string to = msg.IsPublic ? Messages.ForAll : Messages.ForYou;
            Console.WriteLine($"{msg.From.Nick} {Messages.Said} {to}: {msg.Message}");
            Console.WriteLine(string.Empty);
        }
    }
}
