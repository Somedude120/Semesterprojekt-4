using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class GetFriends //: IGetFriends
    {
        private static IUnitOfWork unitOfWork;

        public GetFriends()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static List<string> GetFriendList(string Username)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
            var friendlist = unitOfWork.FriendList.GetAll();

            var returnList = new List<string>();
            foreach (var friend in friendlist)
            {
                if(friend.User1 == Username)
                    returnList.Add(friend.User2);

                else if (friend.User2 == Username)
                    returnList.Add(friend.User1);
            }

            return returnList;
            
        }

    }
}
