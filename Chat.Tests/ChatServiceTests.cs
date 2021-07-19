using Chat.Business.Services;
using Chat.Common;
using Chat.Domain.Contracts;
//using Chat.Infraestructure;
using Chat.Infraestructure.Helpers.RestSharp;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Chat.IntegrationTests
{
    public class ChatServiceTests
    {
        #region Ctor
        ServiceProvider serviceProvider;
        public ChatServiceTests()
        {
            serviceProvider = new ServiceCollection()
                    //.AddSingleton<IRabbitMQConn, RabbitMQConn>()
                    //.AddSingleton<IRabbitMQWrapper, RabbitMQWrapper>()
                    .AddSingleton<IRestSharpWrapper>(x => ActivatorUtilities.CreateInstance<RestSharpWrapper>(x, new[] { "Basic ZG1henpvbGxhOkhlaXRvcio=", "http://mazzolla.eastus2.cloudapp.azure.com:15672/api" }))
                    .AddSingleton<IChatService>(x => ActivatorUtilities.CreateInstance<ChatService>(x, new[] { "chat" }))
               .BuildServiceProvider();
        }
        #endregion

        [Fact]
        public void CreateVhostChat_CriarVhostNoServidorDeMensageria_RetornaMensagemDeErroNula()
        {
            string strReturn = serviceProvider.GetService<IChatService>().CreateVhostChat();

            Assert.True(strReturn.IsNullOrEmptyOrWhiteSpace());
        }

        [Fact]
        public void GetUsers_ObterListaDeUsuários_RetornaObjetoDiferenteDeNulo()
        {
            List<Domain.User> users = serviceProvider.GetService<IChatService>().GetUsers();

            Assert.True(users != null);
        }



    }
}
