using System.Collections.Generic;

namespace Chat.Domain.Contracts
{
    public interface IChatService
    {
        string CreateVhostChat();
        List<User> GetUsers();
    }
}
