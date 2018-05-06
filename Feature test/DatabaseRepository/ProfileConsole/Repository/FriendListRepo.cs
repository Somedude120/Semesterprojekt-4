using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class FriendListRepo
    {
        public void CreateFriendInformation(string user1, string user2, string status, string action_user)
        {
            using (var db = new BloggingContext())
            {
                var friendList = new FriendList { User1 = user1, User2 = user2, Status = status, Action_User = action_user };
                db.FriendList.Add(friendList);
                db.SaveChanges();

                Console.WriteLine("Created friendstatus between: " + friendList.User1 + " and " + friendList.User2 + " to " + friendList.Status + "\n");
            }
        }

        public void ReadFriendList()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.FriendList
                            orderby b.Status
                            select b;

                Console.WriteLine("All friendstatuses in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Status + ": " + item.User1 + " and " + item.User2 + ", last action made by: " + item.Action_User);
                }
                Console.WriteLine("\n");
            }
        }

        public void DeleteFriendInformation(string user1, string user2)
        {
            using (var db = new BloggingContext())
            {
                var deleteFriendInformation =
                    from p in db.FriendList
                    where p.User1 == user1 && p.User2 == user2
                    select p;

                foreach (var friendInformation in deleteFriendInformation)
                {
                    db.FriendList.Remove(friendInformation);
                    Console.WriteLine("Deleted friendInformation between " + friendInformation.User1 + " and " + friendInformation.User2 + " from the database\n");
                }

                try
                {
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
