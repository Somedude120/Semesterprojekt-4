using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class GetFriends : IGetFriends
    {
        private IUnitOfWork unitOfWork;

        public GetFriends()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public List<String> GetFriendList(string Username)
        {
            var person = unitOfWork.UserInformation.GetString(Username);
            List<String> tempFriendList = new List<string>();
            if (person.UserName == Username)
            {
                using (var db = new ProfileContext())
                {
                    var profile =
                        from p in db.UserInformation
                        where p.UserName == Username
                        select p;

                    try
                    {
                        foreach (var pers in profile)
                        {
                            foreach (var friend in pers.FriendList)
                            {
                                if (Username == friend.User1)
                                tempFriendList.Add(friend.User2);
                                

                                else if (Username == friend.User2)
                                    tempFriendList.Add(friend.User1);

                            }

                            return tempFriendList;
                        }
                    }

                    catch (Exception)
                    {
                        return null;
                    }

                }

            }
            return null;
        }

    }
}
