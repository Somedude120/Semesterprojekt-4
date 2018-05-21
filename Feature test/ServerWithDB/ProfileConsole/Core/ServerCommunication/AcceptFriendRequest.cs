using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    public class AcceptFriendRequest : IAcceptFriendRequest
    {
        private IUnitOfWork unitOfWork;

        public AcceptFriendRequest()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public void AcceptRequest(string Username, string newFriend)
        {
            var friendList = unitOfWork.FriendList.GetAll();

            foreach (var friend in friendList)
            {
                if (friend.User1 == newFriend && friend.User2 == Username)
                {
                    friend.User1 = newFriend;
                    friend.User2 = Username;
                    friend.Status = "Added";
                    friend.Action_User = Username;
                    unitOfWork.Complete();
                }
            }
            
        }

    }
}
