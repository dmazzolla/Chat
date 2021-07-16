using Chat.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        static List<User> listUsers = new List<User>();

        private ILogger<ChatController> _logger;
        ConnectionFactory factory;

        public ChatController(ILogger<ChatController> logger)
        {
            _logger = logger;
            factory = new ConnectionFactory() { HostName = "localhost" };
            listUsers.Add(new User { Nick = "all" });
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(Domain.User user)
        {
            if (listUsers.Where(x => x.Nick.Equals(user.Nick)).Count() == 0)
            {
                registerUser(user);
                return new StatusCodeResult(201);
            }
            else
                return Ok();
        }

        private void registerUser(User user)
        {
            listUsers.Add(user);
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: user.Nick,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            }
        }

        [HttpGet]
        [Route("Queues")]
        public ActionResult<List<Queue>> Queues()
        {
            RestClient client = new RestClient("http://localhost:15672/api/queues");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Basic Z3Vlc3Q6Z3Vlc3Q=");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<List<Queue>>(response.Content);
            else
                return new StatusCodeResult((int)response.StatusCode);
        }


        [HttpPost]
        [Route("Message")]
        public IActionResult Message(Domain.Chat chat)
        {
            if (listUsers.Where(x => x.Nick.Equals(chat.To.Nick)).Count() == 0)
            {
                return new StatusCodeResult(204);
            }

            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    string message = JsonConvert.SerializeObject(chat);
                    byte[] body = Encoding.UTF8.GetBytes(message);

                    if (chat.To.Nick.Equals("all"))
                    {
                        foreach (var item in listUsers)
                        {
                            channel.BasicPublish(
                                exchange: "",
                                routingKey: item.Nick,
                                basicProperties: null,
                                body: body);
                        }
                    }
                    else
                    {
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: chat.To.Nick,
                            basicProperties: null,
                            body: body);
                    }
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao tentar registrar um novo usuário", ex);
                return new StatusCodeResult(500);
            }
        }
    }
}
