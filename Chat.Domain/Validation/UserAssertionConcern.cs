using Chat.Common;
using Chat.Common.Resources;
using System;
using System.Collections.Generic;

namespace Chat.Domain.Validation
{
    public class UserAssertionConcern
    {
        public static void User(string nickName)
        {
            if (nickName.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidOperationException(Errors.EnterNickName);
            }
        }

        public static void Members(User user, List<User> lstMembers)
        {
            foreach (var item in lstMembers)
            {
                if (item.Equals(user))
                    throw new InvalidOperationException(Errors.UserMembersValidate);
            }
        }
    }
}
