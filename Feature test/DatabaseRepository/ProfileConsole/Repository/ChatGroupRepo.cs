using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class ChatGroupRepo
    {
        public void CreateChatGroup(string groupName)
        {
            using (var db = new BloggingContext())
            {
                var group = new ChatGroups { GroupName = groupName};
                db.ChatGroups.Add(group);
                db.SaveChanges();

                Console.WriteLine("Added chatGroup: " + groupName + " to the database\n");
            }
        }

        public void ReadChatGroups()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.ChatGroups
                    orderby b.GroupName
                    select b;

                Console.WriteLine("All chatGroups in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.GroupName);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
