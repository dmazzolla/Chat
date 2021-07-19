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
using System.Threading;

namespace Chat.App
{
    class Program
    {
        private static int _qttyMembers = 0;        
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                    .AddSingleton<IRabbitMQConn, RabbitMQConn>()
                    .AddSingleton<IRabbitMQWrapper, RabbitMQWrapper>()
                    .AddSingleton<IRestSharpWrapper, RestSharpWrapper>()
                    .AddSingleton<IChatService, ChatService>()
               .BuildServiceProvider();

            WriteWelcomeMessage();

            string msg = serviceProvider.GetService<IChatService>().CreateVhostChat();
            if (msg.IsNotNullOrEmptyOrWhiteSpace())
            {
                Console.WriteLine(msg);
                Environment.Exit(-1);
            }

            WriteAlphanumericFormatNickNameMessage();

            List<User> lstMembers = serviceProvider.GetService<IChatService>().GetUsers();
            User user = ReadNickName(lstMembers);
            lstMembers.Add(user);
            _qttyMembers = lstMembers.Count;

            WriteSamplesSendMessage(user);

            WriteMembersChat(lstMembers);

            IRabbitMQWrapper serviceChat = serviceProvider.GetService<IRabbitMQWrapper>();

            serviceChat.Listen(user.NickName).Received += ListenIncomingMessages;

            new Timer(ListenNewMembers, serviceProvider, 0, 60000);

            ListenSendMessages(serviceProvider, user, serviceChat);
        }



        #region InterfaceSupportFunctions

        private static void ListenNewMembers(object o)
        {
            ServiceProvider serviceProvider = (ServiceProvider)o;

            var members = serviceProvider.GetService<IChatService>().GetUsers();
            int qttyMembers = members.Count;

            if (_qttyMembers != qttyMembers)
            {
                WriteMembersChat(members, true);
                _qttyMembers = qttyMembers;
            }
        }

        private static void ListenSendMessages(ServiceProvider serviceProvider, User user, IRabbitMQWrapper serviceChat)
        {
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

                serviceChat.Submit(chatMessage);
                Console.WriteLine(string.Empty);
            }
        }

        private static void WriteSamplesSendMessage(User user)
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Format(Messages.UserWelcome, user.NickName));
            Console.WriteLine(string.Empty);
            Console.WriteLine(Messages.ExamplesSendMsg);
            Console.WriteLine(Messages.ExamplePrivateMsg);
            Console.WriteLine(Messages.ExamplePublicMsg);
            Console.WriteLine(string.Empty);
        }

        private static User ReadNickName(List<User> lstMembers)
        {
            User user;
            while (true)
            {
                Console.WriteLine(Messages.EnterYourNickname);
                user = new User(Console.ReadLine().EnsureAlphaNumeric().ToUpper());
                try
                {
                    user.Validate();
                    user.ValidateNewUserInMembersList(lstMembers);
                    break;
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(string.Empty);
                    continue;
                }
            }

            return user;
        }

        private static void WriteAlphanumericFormatNickNameMessage()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(Messages.AlphanumericOnly);
            Console.WriteLine(string.Empty);
        }

        private static void WriteMembersChat(List<User> lstMembers, bool newUser = false)
        {
            if (lstMembers.Count == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(Messages.NoUsers);
                Console.WriteLine(string.Empty);                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(newUser ? Messages.NewUsersChat : Messages.UsersChat);
                Console.WriteLine(string.Empty);
                foreach (var item in lstMembers)
                {
                    Console.WriteLine(item.NickName);
                }
                Console.WriteLine(string.Empty);
            }
            Console.ResetColor();
            Console.WriteLine(Messages.EnterMsg);
            Console.WriteLine(string.Empty);
        }

        private static void WriteWelcomeMessage()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Messages.Welcome);
            Console.WriteLine(string.Empty);
            Console.ResetColor();
        }

        private static void ListenIncomingMessages(object sender, BasicDeliverEventArgs e)
        {
            string jsonMsg = Encoding.UTF8.GetString(e.Body.ToArray());

            ChatMessageModel msg = JsonConvert.DeserializeObject<ChatMessageModel>(jsonMsg);
            string to = msg.IsPublic ? Messages.ForAll : Messages.ForYou;

            if (msg.IsPublic)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"{msg.From.NickName} {Messages.Said} {to}: {msg.Message}");
            Console.ResetColor();
            Console.WriteLine(string.Empty);
        }

        #endregion
    }
}
