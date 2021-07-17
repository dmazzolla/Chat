using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Common;
using Chat.Common.Resources;

namespace Chat.Domain.Validation
{
    public class ChatMessageAssertionConcern
    {
        public static void Members(List<User> members)
        {
            if (members == null || members.Count == 0)
            {
                throw new InvalidOperationException(Errors.ChatRoomWithoutMembers);
            }
        }

        public static void Message(string message)
        {
            if (message.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidOperationException(Messages.EnterMsg);                
            }
        }

        public static void CheckRecipient(bool isPublic, List<User> members, User to)
        {
            if (!isPublic && members.Where(x => x.NickName.Equals(to.NickName)).Count() == 0)
            {
                throw new InvalidOperationException(Errors.RecipientNotFound);
            }
        }

    }
}
