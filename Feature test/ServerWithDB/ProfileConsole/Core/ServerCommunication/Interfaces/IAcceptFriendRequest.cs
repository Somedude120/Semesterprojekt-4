using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    public interface IAcceptFriendRequest
    {
        void AcceptRequest(string Username, string newFriend);
    }
}
