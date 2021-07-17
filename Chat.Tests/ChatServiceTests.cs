using Chat.Business.Services;
using Chat.Common;
using Chat.Domain.Contracts;
//using Chat.Infraestructure;
using Chat.Infraestructure.Helpers.RestSharp;
using Microsoft.Extensions.DependencyInjection;
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
                    .AddSingleton<IRestSharpWrapper>(x => ActivatorUtilities.CreateInstance<RestSharpWrapper>(x, new[] { "Basic Z3Vlc3Q6Z3Vlc3Q=", "http://localhost:15672/api" }))
                    .AddSingleton<IChatService>(x => ActivatorUtilities.CreateInstance<ChatService>(x, new[] { "chat" }))
               .BuildServiceProvider();
        }
        #endregion

        [Fact]
        public void Criar_VhostNoServidorDeMensageria_RetornaMensagemDeErroNula()
        {            
            Assert.True(serviceProvider.GetService<IChatService>().CreateVhostChat().IsNullOrEmptyOrWhiteSpace());
        }

        [Fact]
        public void Obter_ListaDeUsuários_RetornaObjetoDiferenteDeNulo()
        {
            Assert.True(serviceProvider.GetService<IChatService>().GetUsers() != null);
        }



    }
}
