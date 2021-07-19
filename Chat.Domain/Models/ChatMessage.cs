using Chat.Common;
using Chat.Common.Resources;
using Chat.Domain.Validation;
using Chat.Model;
using System;
using System.Collections.Generic;

namespace Chat.Domain
{
    public class ChatMessage
    {
        #region Ctor

        public ChatMessage(ChatMessageModel model)
        {
            From = new User(model.From);
            To = new User(model.To);

            Members = new List<User>();
            model.Members.ForEach(x => { Members.Add(new User(x.NickName)); });

            Message = model.Message;
            IsPublic = model.IsPublic;
        }

        public ChatMessage(User from, User to, List<User> members, string message, bool isPublic)
        {
            From = from;
            To = to;
            Members = members;
            Message = message;
            IsPublic = isPublic;
        }

        public ChatMessage(string userInput, string from, List<User> members)
        {
            string[] split = userInput.Split("=>");
            Message = split[0];

            //User to = (split.Length > 1 && split[1].IsNotNullOrEmptyOrWhiteSpace()) ? new User(split[1]) : null;
            User to = split.Length > 1 ? new User(split[1]) : null;
            IsPublic = (to == null);

            To = to;
            From = new User(from);
            Members = members;
        }

        #endregion

        #region Properties

        public User From { get; private set; }
        public User To { get; private set; }
        public List<User> Members { get; private set; }
        public string Message { get; private set; }
        public bool IsPublic { get; private set; }
        public bool IsPrivate => !IsPublic;
        #endregion

        public void Validate()
        {
            AssertionConcern.AssertArgumentNotNull(From, Errors.SenderNotFound);
            From.Validate();
            if (IsPrivate)
            {
                AssertionConcern.AssertArgumentNotNull(To, Errors.RecipientNotFound);
                To.Validate();                
            }
            ChatMessageAssertionConcern.MessageHasContent(Message);
            ChatMessageAssertionConcern.ExistsAnothersMembersInChat(From, Members);            
            if (IsPrivate)
                ChatMessageAssertionConcern.RecipientIsAmongMembers(To, Members);
        }

        public override bool Equals(object obj)
        {
            return obj is ChatMessage chat &&
                   EqualityComparer<User>.Default.Equals(From, chat.From) &&
                   EqualityComparer<User>.Default.Equals(To, chat.To) &&
                   EqualityComparer<List<User>>.Default.Equals(Members, chat.Members) &&
                   Message == chat.Message &&
                   IsPublic == chat.IsPublic;
        }

        public bool NotEquals(object obj)
        {
            return !Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To, Members, Message, IsPublic);
        }
    }
}
