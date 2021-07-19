using Chat.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace Chat.UnitTests
{
    public class UserTest
    {
        [Theory]
        [InlineData("USER1")]
        [InlineData("USER2")]
        public void User_CriarUsuarioDoChat_RetornaUmObjetoUser(string nickName)
        {
            User user = new User(nickName);

            user.Validate();

            Assert.NotNull(user);
        }


        [Theory]
        [InlineData("ZÉ")]
        [InlineData("MÁ")]
        public void User_CriarUsuarioDoChat_RetornaErroPeloFatoDoNickNameNaoEstarNoFormatoEsperado(string nickName)
        {
            User user = new User(nickName);

            Assert.Throws<InvalidOperationException>(delegate
            {
                user.Validate();
            });
        }

        [Theory]
        [InlineData("DANIEL", "USER1,USER2,USER3,USER4,USER5")]
        [InlineData("HEITOR", "USER1,USER2,USER3,USER4,USER5")]
        public void User_CriarUsuarioValidandoSeJaExisteUmUsuarioAtivoNoChat_RetornaUmObjetoUserPeloFatoDeNaoExistir(string nickName, string members)
        {
            User user = new User(nickName);

            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            user.Validate();
            user.ValidateNewUserInMembersList(listChatMembers);
            
            Assert.NotNull(user);
        }


        [Theory]
        [InlineData("USER1", "USER1,USER2,USER3,USER4,USER5")]
        [InlineData("USER5", "USER1,USER2,USER3,USER4,USER5")]
        public void User_CriarUsuarioValidandoSeJaExisteUmUsuarioAtivoNoChatComOMesmoNickName_RetornaErroPeloFatoDeJaExistir(string nickName, string members)
        {
            User user = new User(nickName);

            List<User> listChatMembers = new List<User>();
            foreach (var item in members.Split(','))
            {
                listChatMembers.Add(new User(item));
            }

            Assert.Throws<InvalidOperationException>(delegate
            {
                user.Validate();
                user.ValidateNewUserInMembersList(listChatMembers);
            });
        }



    }
}
