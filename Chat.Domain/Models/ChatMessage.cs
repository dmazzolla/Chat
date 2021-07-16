﻿using Chat.Common;
using Chat.Domain.Validation;
using Chat.Model;
using System;
using System.Collections.Generic;

namespace Chat.Domain
{
    public class ChatMessage
    {
        #region Ctor

        public ChatMessage(MsgChatModel model)
        {
            From = new User(model.From);
            To = new User(model.To);
            model.Members.ForEach(x => { Members.Add(new User(x.Nick)); });
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

            User to = (split.Length > 1 && split[1].IsNotNullOrEmptyOrWhiteSpace()) ? new User(split[1]) : null;
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

        #endregion

        public void Validate()
        {
            ChatMessageAssertionConcern.Members(Members);
            ChatMessageAssertionConcern.Message(Message);
            ChatMessageAssertionConcern.CheckMemberTo(IsPublic, Members, To);
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