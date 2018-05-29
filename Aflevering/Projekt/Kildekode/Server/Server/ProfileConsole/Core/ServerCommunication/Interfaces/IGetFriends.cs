using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    public interface IGetFriends
    {
        List<String> GetFriendList(string Username);
    }
}
