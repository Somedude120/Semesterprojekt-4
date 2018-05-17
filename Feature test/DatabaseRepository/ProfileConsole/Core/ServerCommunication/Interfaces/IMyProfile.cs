using ProfileConsole.Core.Domain;
using System.Collections.Generic;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    public interface IMyProfile
    {
        string username { get; set; }
        string description { get; set; }
        string status { get; set; }
        ICollection<FriendList> friendlist { get; set; }
        ICollection<Tags> tags { get; set; }
    }
}
