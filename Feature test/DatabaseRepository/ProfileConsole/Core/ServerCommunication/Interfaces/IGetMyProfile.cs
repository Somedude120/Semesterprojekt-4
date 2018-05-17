using ProfileConsole.Core.Domain;
using System.Collections.Generic;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    interface IGetMyProfile
    {
        MyProfile RequestOwnInformation(string Username, string Description, string Status,
            ICollection<FriendList> FriendList, ICollection<Tags> Tags);
    }
}
