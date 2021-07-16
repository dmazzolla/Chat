using Chat.Domain.Contracts;
using System.Net;
using Chat.Common.Resources;
using System.Collections.Generic;
using Chat.Domain;
using Chat.Model;
using System;
using System.Configuration;

namespace Chat.Business.Services
{
    public class ChatService : IChatService
    {
        private IRestSharpWrapper _restSharpWrapper;
        public ChatService(IRestSharpWrapper restSharpWrapper)
        {
            _restSharpWrapper = restSharpWrapper;
        }

        public string CreateVhostChat()
        {
            HttpStatusCode statusCode = _restSharpWrapper.ExecutRequest("vhosts", RestSharp.Method.PUT, ConfigurationManager.AppSettings["RabbitMQVHost"]);
            if (statusCode == HttpStatusCode.NotFound)
            {
                return Errors.CreateVhostNotFound;
            }
            else if ((int)statusCode >= 400 && (int)statusCode < 500)
            {
                return Errors.CreateVhostCouldnt;
            }
            else if (statusCode == HttpStatusCode.GatewayTimeout)
            {
                return Errors.CreateVhostGatewayTimeout;
            }
            else if ((int)statusCode >= 500)
            {
                return Errors.CreateVhostChatInternalError;
            }

            return string.Empty;
        }

        public List<User> GetUsers()
        {
            HttpStatusCode statusCode;
            List<QueueModel> lstQueues = _restSharpWrapper.ExecutRequest<List<QueueModel>>("queues", out statusCode, RestSharp.Method.GET, ConfigurationManager.AppSettings["RabbitMQVHost"]);
            List<User> lstMembers = new List<User>();
            
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                foreach (var item in lstQueues)
                {            
                    lstMembers.Add(new User(item.Name));

                    //tentar limpar os usuários (queues) que estão inativos há mais de 10 minutos...
                    if (DateTime.UtcNow.AddMinutes(-10) > item.IdleSince )
                        _restSharpWrapper.ExecutRequest($"queues/{ConfigurationManager.AppSettings["RabbitMQVHost"]}", RestSharp.Method.DELETE, item.Name);
                }
            }
            return lstMembers;
        }

    }
}
