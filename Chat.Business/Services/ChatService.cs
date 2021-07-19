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
        private readonly IRestSharpWrapper _restSharpWrapper;
        private readonly string _vhost;

        public ChatService(IRestSharpWrapper restSharpWrapper)
        {
            _restSharpWrapper = restSharpWrapper;
            _vhost = ConfigurationManager.AppSettings["RabbitMQVHost"];
        }

        public ChatService(IRestSharpWrapper restSharpWrapper, string vhost)
        {
            _restSharpWrapper = restSharpWrapper;
            _vhost = vhost;
        }

        public string CreateVhostChat()
        {
            HttpStatusCode statusCode = _restSharpWrapper.ExecutRequest("vhosts", RestSharp.Method.PUT, _vhost);
            if ((int)statusCode == 0 || statusCode == HttpStatusCode.NotFound)
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

            return null;
        }

        public List<User> GetUsers()
        {
            HttpStatusCode statusCode;
            List<User> lstMembers = null;
            List<QueueModel> lstQueues = _restSharpWrapper.ExecutRequest<List<QueueModel>>("queues", out statusCode, RestSharp.Method.GET, ConfigurationManager.AppSettings["RabbitMQVHost"]);

            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                lstMembers = new List<User>();
                foreach (var item in lstQueues)
                {
                    lstMembers.Add(new User(item.Name));

                    ////tentar limpar os usuários (queues) que estão inativos há mais de 10 minutos...
                    //if (DateTime.UtcNow.AddMinutes(-10) > item.IdleSince)
                    //    _restSharpWrapper.ExecutRequest($"queues/{ConfigurationManager.AppSettings["RabbitMQVHost"]}", RestSharp.Method.DELETE, item.Name);
                }                
            }
            return lstMembers;
        }
    }
}
