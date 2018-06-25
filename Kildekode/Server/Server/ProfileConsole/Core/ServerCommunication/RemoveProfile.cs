using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    public class RemoveProfile //: IAddFriend
    {
        private static IUnitOfWork unitOfWork;

        public RemoveProfile()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static void RemoveProfileRequest(string Username)
        {
            Console.WriteLine("Remove profile called");
            unitOfWork = new UnitOfWork(new ProfileContext());
            var person = unitOfWork.UserInformation.GetString(Username);
            var messages = unitOfWork.Chat.GetAll();
            var logininfo = unitOfWork.LoginRepo.GetAll();
            var userinfo = unitOfWork.UserInformation.GetAll();
            if (person.UserName == Username)
            {
                var friendlist = unitOfWork.FriendList.GetAll();

                try
                {
                    foreach (var message in messages)
                    {
                        if ((message.Sender == Username) || (message.Receiver == Username))
                            unitOfWork.Chat.Remove(message);
                    }

                    foreach (var friend in friendlist)
                    {
                        if((friend.User1 == Username) || (friend.User2 == Username))
                            unitOfWork.FriendList.Remove(friend);

                    }

                    foreach (var relevantLogin in logininfo)
                    {
                        if (relevantLogin.Username == Username)
                            unitOfWork.LoginRepo.Remove(relevantLogin);
                    }

                    foreach (var user in userinfo)
                    {
                        if (user.UserName == Username)
                            unitOfWork.UserInformation.Remove(user);
                    }


                    unitOfWork.Complete();
                }

                catch (Exception e)
                {
                        Console.WriteLine(e);
                }
                


            }
        }
    }
}
