using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    public class RemoveFriend //: IAddFriend
    {
        private static IUnitOfWork unitOfWork;

        public RemoveFriend()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static void RemoveFriendRequest(string Username, string Friend)
        {
            Console.WriteLine("Remove called: " + Username + " " + Friend);
            unitOfWork = new UnitOfWork(new ProfileContext());
            var person = unitOfWork.UserInformation.GetString(Username);
            var messages = unitOfWork.Chat.GetAll();
            if (person.UserName == Username)
            {
                var friendlist = unitOfWork.FriendList.GetAll();

                try
                {
                    foreach (var friend in friendlist)
                    {
                        if((friend.User1 == Username && friend.User2 == Friend) || (friend.User2 == Username && friend.User1 == Friend))
                            unitOfWork.FriendList.Remove(friend);

                    }

                    foreach (var message in messages)
                    {
                        if ((message.Sender == Username && message.Receiver == Friend) || (message.Receiver == Username && message.Sender == Friend))
                            unitOfWork.Chat.Remove(message);
                    }
                    unitOfWork.Complete();
                }

                catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                //Console.WriteLine("User is user");
                //using (var db = new ProfileContext())
                //{
                //    var friendlist =
                //        from p in db.FriendList
                //        where (p.User1 == Username && p.User2 == Friend) || (p.User2 == Username && p.User1 == Friend)
                //        select p;

                //    try
                //    {
                //        foreach (var friend in friendlist)
                //        {
                //            Console.WriteLine(friend.User1 + " " + friend.User2 + " " + friend.Status);
                //            // Note: Attatch to the entity:
                //            unitOfWork.MyTableEntity.Attach(EntityToRemove);
                //            unitOfWork.FriendList.Remove();

                //            unitOfWork.FriendList.Remove(friend);
                //            unitOfWork.Complete();
                //        }

                //    }

                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e);
                //    }

                //}


            }
            }
    }
}
