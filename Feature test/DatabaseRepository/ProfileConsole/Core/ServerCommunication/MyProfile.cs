using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.ServerCommunication
{
    public class MyProfile
    {
        public MyProfile(string Username, string Description, string Status,
            ICollection<FriendList> FriendList, ICollection<Tags> Tags)
        {
            username = Username;
            status = Status;
            friendlist = FriendList;
            tags = Tags;
        }

        public string username;
        public string status;
        public ICollection<FriendList> friendlist;
        public ICollection<Tags> tags;
    }
}
