using System.Collections.Generic;

namespace Chat.Model
{
    public class MsgChatModel
    {
        public UserModel From { get;  set; }
        public UserModel To { get;  set; }
        public List<UserModel> Members { get;  set; }
        public string Message { get;  set; }
        public bool IsPublic { get;  set; } = true;

    }
}
