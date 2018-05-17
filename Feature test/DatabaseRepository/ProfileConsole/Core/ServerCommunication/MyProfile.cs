using ProfileConsole.Core.Domain;
using System.Collections.Generic;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    public class MyProfile : IMyProfile
    {
        public MyProfile(string Username, string Name, string Description, string Status,
            ICollection<FriendList> FriendList, ICollection<Tags> Tags)
        {
            username = Username;
            name = Name;
            description = Description;
            status = Status;
            friendlist = FriendList;
            tags = Tags;
        }

        public string username { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public ICollection<FriendList> friendlist { get; set; }
        public ICollection<Tags> tags { get; set; }
    }
}
