using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class UserChatGroupRepo
    {
        public void CreateUserChatGroup(int groupId, string userName)
        {
            using (var db = new BloggingContext())
            {
                UserInformation u = new UserInformation { UserName = userName };
                //db.UserInformation.Add(u);
                db.UserInformation.Attach(u);

                ChatGroups t = new ChatGroups() { GroupId = groupId};
                db.ChatGroups.Attach(t);

                u.ChatGroups.Add(t);

                db.SaveChanges();

                Console.WriteLine("Added user: " + userName + " to chatGroupId: " + groupId + "\n");
            }
        }

        public void DeleteUserChatGroup(int groupId, string userName)
        {
            using (var db = new BloggingContext())
            {
                var user = db.UserInformation.FirstOrDefault(u => u.UserName == userName);
                var chatGroup = db.ChatGroups.FirstOrDefault(t => t.GroupId == groupId);

                user.ChatGroups.Remove(chatGroup);

                db.SaveChanges();

                Console.WriteLine("Deleted user: " + userName + " from chatGroupId: " + groupId + "\n");
            }
        }
    }
}
