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
            description = Description;
            friendlist = FriendList;
            tags = Tags;
        }

        public string username { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public ICollection<FriendList> friendlist { get; set; }
        public ICollection<Tags> tags { get; set; }
    }
}
