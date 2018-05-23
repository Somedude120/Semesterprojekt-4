using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    public class AddFriend //: IAddFriend
    {
        private static IUnitOfWork unitOfWork;

        public AddFriend()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static void AddFriendRequest(string Username, string newFriend)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
            var person = unitOfWork.UserInformation.GetString(Username);

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

                            var friendlist = new FriendList
                            {
                                User1 = Username,
                                User2 = newFriend,
                                Status = "Pending",
                                Action_User = Username
                            };

                            unitOfWork.FriendList.Add(friendlist);
                            unitOfWork.Complete();
                            
                        }
                        
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }


            }
        }
    }
}
