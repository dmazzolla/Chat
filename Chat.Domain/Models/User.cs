using Chat.Common.Resources;
using Chat.Domain.Validation;
using Chat.Model;
using System;
using System.Collections.Generic;

namespace Chat.Domain
{
    public class User
    {
        #region Ctor
        protected User() { }

        public User(string nickName)
        {
            NickName = nickName;
        }
        public User(UserModel model)
        {
            NickName = model.NickName;
        }
        #endregion


        #region Properties

        public string NickName { get; private set; }

        #endregion

        public void Validate()
        {
            //AssertionConcern.AssertArgumentNotNull(NickName, Errors.EnterNickName);
            UserAssertionConcern.User(NickName);
            AssertionConcern.AssertArgumentLength(NickName, 3, 20, Errors.EnterNickName);
            AssertionConcern.AssertArgumentMatches("^[a-zA-Z][a-zA-Z0-9]*$", NickName, Errors.EnterNickName);
        }

        public void ValidateNewUserInMembersList(List<User> lstMembers)
        {
            UserAssertionConcern.Members(this, lstMembers);
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   NickName == user.NickName;
        }

        public bool NotEquals(object obj)
        {
            return !Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NickName);
        }



    }



}
