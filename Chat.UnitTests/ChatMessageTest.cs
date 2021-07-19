using Chat.Domain;
using Chat.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Chat.UnitTests
{
    public class ChatMessageTest
    {
        [Theory]
        [InlineData("USER2", "USER1", "USER1,USER2", "mensagem privada 2", false)]
        [InlineData("USER3", "USER5", "USER1,USER2,USER3,USER4,USER5", "mensagem privada", false)]
        public void ChatMessage_CriarMensagemPrivada_RetornaUmObjetoChatMessage(string from, string to, string members, string message, bool isPublic)
        {
            // Arrange
            User userFrom = new User(from);
            userFrom.Validate();

            User userTo = new User(to);
            userTo.Validate();

            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(userFrom, userTo, listChatMembers, message, isPublic);
            chatMessage.Validate();

            // Assert
            Assert.NotNull(chatMessage);
        }

        [Theory]
        [InlineData("USER1", "USER1,USER2,USER3,USER4,USER5", "mensagem publica", true)]
        [InlineData("USER3", "USER1,USER2,USER3", "mensagem publica 2", true)]
        public void ChatMessage_CriarMensagemPublica_RetornaUmObjetoChatMessage(string from, string members, string message, bool isPublic)
        {
            // Arrange
            User userFrom = new User(from);
            userFrom.Validate();

            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(userFrom, null, listChatMembers, message, isPublic);
            chatMessage.Validate();

            // Assert
            Assert.NotNull(chatMessage);
        }

        [Theory]
        [InlineData("USER1", "USER1,USER2", null, false)]
        [InlineData("USER2", "USER1,USER2,USER3,USER4,USER5", "", false)]
        public void ChatMessage_CriarMensagemPrivada_RetornaErroPeloFatoDaMensagemNaoTerRemetente(string to, string members, string message, bool isPublic)
        {
            // Arrange
            User userTo = new User(to);
            userTo.Validate();

            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(null, userTo, listChatMembers, message, isPublic);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });
        }

        [Theory]
        [InlineData("USER2", "USER1,USER2", "mensagem privada 1", false)]
        [InlineData("USER3", "USER1,USER2,USER3,USER4,USER5", "mensagem privada 2", false)]
        public void ChatMessage_CriarMensagemPrivada_RetornaErroPeloFatoDaMensagemNaoTerDestinatario(string from, string members, string message, bool isPublic)
        {
            // Arrange
            User userFrom = new User(from);
            userFrom.Validate();

            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(userFrom, null, listChatMembers, message, isPublic);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });
        }

        [Theory]
        [InlineData("USER2", "USER3", "USER1,USER2", "mensagem privada 1", false)]
        [InlineData("USER3", "USER6", "USER1,USER2,USER3,USER4,USER5", "mensagem privada 2", false)]
        public void ChatMessage_CriarMensagemPrivada_RetornaErroPeloFatoDoDestinarioNaoEstarPresenteNaListaDeMembros(string from, string to, string members, string message, bool isPublic)
        {
            // Arrange
            User userFrom = new User(from);
            userFrom.Validate();

            User userTo = new User(to);
            userTo.Validate();

            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(userFrom, userTo, listChatMembers, message, isPublic);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });
        }

        [Theory]
        [InlineData("USER2", "USER1", "USER1,USER2", null, false)]
        [InlineData("USER3", "USER2", "USER1,USER2,USER3,USER4,USER5", "", false)]
        public void ChatMessage_CriarMensagemPrivada_RetornaErroPeloFatoDaMensagemNaoTerConteudo(string from, string to, string members, string message, bool isPublic)
        {
            // Arrange
            User userFrom = new User(from);
            userFrom.Validate();

            User userTo = new User(to);
            userTo.Validate();

            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(userFrom, userTo, listChatMembers, message, isPublic);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });
        }

        [Theory]
        [InlineData("USER2", "USER1", "USER2", "mensagem privada 1", false)]
        [InlineData("USER3", "USER2", "USER3", "mensagem privada 2", false)]
        public void ChatMessage_CriarMensagemPrivada_RetornaErroPeloFatoDeNaoHaverOutrosUsuariosNoChat(string from, string to, string members, string message, bool isPublic)
        {
            // Arrange
            User userFrom = new User(from);
            userFrom.Validate();

            User userTo = new User(to);
            userTo.Validate();

            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(userFrom, userTo, listChatMembers, message, isPublic);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });
        }

        [Theory]
        [InlineData("USER1", "USER2", "USER1,USER2,USER3,USER4,USER5", "mensagem publica", true)]
        [InlineData("USER3", "USER1", "USER1,USER2,USER3", "mensagem publica 2", true)]
        public void ChatMessage_CriarMensagemPrivadaAPartirDeUmModel_RetornaUmObjetoChatMessage(string from, string to, string members, string message, bool isPublic)
        {
            // Arrange
            UserModel userFrom = new UserModel { NickName = from };
            UserModel userTo = new UserModel { NickName = to };


            List<UserModel> listChatMembers = new List<UserModel>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new UserModel { NickName = item });
            }

            ChatMessageModel chatMessageModel = new ChatMessageModel
            {
                From = userFrom,
                IsPublic = isPublic,
                Members = listChatMembers,
                Message = message,
                To = userTo
            };

            // Act
            ChatMessage chatMessage = new ChatMessage(chatMessageModel);
            chatMessage.Validate();

            // Assert
            Assert.NotNull(chatMessage);
        }

        [Theory]
        [InlineData("USER1", "mensagem publica", "USER1,USER2,USER3,USER4,USER5")]
        [InlineData("USER3", "mensagem privada=> USER1 ", "USER1,USER2,USER3")]
        public void ChatMessage_CriarMensagemAPartirDeUmaStringObtidaPeloConsoleReadLine_RetornaUmObjetoChatMessage(string from, string consoleReadLine, string members)
        {
            // Arrange
            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(consoleReadLine, from, listChatMembers);
            chatMessage.Validate();

            // Assert
            Assert.NotNull(chatMessage);
        }

        [Theory]
        [InlineData("", "mensagem privada=>USER1", "USER1,USER2,USER3")]
        [InlineData("", "mensagem privada=>USER2", "USER1,USER2,USER3")]
        public void ChatMessage_CriarMensagemAPartirDeUmaStringObtidaPeloConsoleReadLine_RetornaErroPeloFatoDaMensagemNaoTerRemetente(string from, string consoleReadLine, string members)
        {
            // Arrange
            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(consoleReadLine, from, listChatMembers);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });
        }

        [Theory]
        
        [InlineData("USER1", "mensagem privada=>  ", "USER1,USER2,USER3")]
        [InlineData("USER2", "mensagem privada=>çç~~", "USER1,USER2,USER3")]
        [InlineData("USER3", "mensagem privada=>~**&&", "USER1,USER2,USER3")]
        public void ChatMessage_CriarMensagemPrivadaAPartirDeUmaStringObtidaPeloConsoleReadLine_RetornaErroPeloFatoDaMensagemNaoTerDestinatarioOuConterValorInvalidoNoComando(string from, string consoleReadLine, string members)
        {
            // Arrange
            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(consoleReadLine, from, listChatMembers);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });                 
        }

        [Theory]
        [InlineData("USER1", "mensagem privada=> USER4 ", "USER1,USER2")]
        [InlineData("USER2", "mensagem privada=> USER5 ", "USER1,USER2,USER3")]
        public void ChatMessage_CriarMensagemAPartirDeUmaStringObtidaPeloConsoleReadLine_RetornaErroPeloFatoDoDestinarioNaoEstarPresenteNaListaDeMembros(string from, string consoleReadLine, string members)
        {
            // Arrange
            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(consoleReadLine, from, listChatMembers);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });
        }

        [Theory]
        [InlineData("USER2", "=>USER1", "USER1,USER2,USER3")]
        [InlineData("USER3", " =>USER2", "USER1,USER2,USER3")]
        public void ChatMessage_CriarMensagemAPartirDeUmaStringObtidaPeloConsoleReadLine_RetornaErroPeloFatoDaMensagemNaoTerConteudo(string from, string consoleReadLine, string members)
        {
            // Arrange
            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(consoleReadLine, from, listChatMembers);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });
        }

        [Theory]
        [InlineData("USER1", "mensagem privada=> USER5 ", "USER1")]
        [InlineData("USER2", "mensagem privada=> USER6 ", "USER2")]
        public void ChatMessage_CriarMensagemAPartirDeUmaStringObtidaPeloConsoleReadLine_RetornaErroPeloFatoDeNaoHaverOutrosUsuariosNoChat(string from, string consoleReadLine, string members)
        {
            // Arrange
            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            // Act
            ChatMessage chatMessage = new ChatMessage(consoleReadLine, from, listChatMembers);

            // Assert  
            Assert.Throws<InvalidOperationException>(delegate
            {
                chatMessage.Validate();
            });
        }
    }
}
