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
            NickName = model.Nick;
        }
        #endregion

        
        #region Properties
        
        public string NickName { get; private set; }

        #endregion

        public void Validate()
        {
            UserAssertionConcern.User(NickName);
        }

        public void ValidateWithMembers(List<User> lstMembers)
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
