using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Common;
using Chat.Common.Resources;

namespace Chat.Domain.Validation
{
    public class ChatMessageAssertionConcern
    {
        public static void ExistsAnothersMembersInChat(User from, List<User> members)
        {
            if (members == null || members.Where(x => x.NotEquals(from)).Count() == 0)
            {
                throw new InvalidOperationException(Errors.ChatRoomWithoutMembers);
            }
        }

        public static void MessageHasContent(string message)
        {
            if (message.IsNullOrEmptyOrWhiteSpace())
            {
                throw new InvalidOperationException(Messages.EnterMsg);
            }
        }

        public static void RecipientIsAmongMembers(User to, List<User> members)
        {
            if (members.Where(x => x.Equals(to)).Count() == 0)
            {
                throw new InvalidOperationException(Errors.RecipientIsntAmongMembers);
            }
        }

    }
}
