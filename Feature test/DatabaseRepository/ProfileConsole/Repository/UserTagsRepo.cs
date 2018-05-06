using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class UserTagsRepo
    {
        public void CreateUserTag(string tagName, string userName)
        {
            using (var db = new BloggingContext())
            {
                UserInformation u = new UserInformation {UserName = userName};
                //db.UserInformation.Add(u);
                db.UserInformation.Attach(u);

                Tags t = new Tags {TagName = tagName};
                db.Tags.Attach(t);

                u.Tags.Add(t);

                db.SaveChanges();

                Console.WriteLine("Added tag: " + tagName + " to user: " + userName + "\n");
            }
        }

        public void DeleteUserTag(string tagName, string userName)
        {
            using (var db = new BloggingContext())
            {
                var user = db.UserInformation.FirstOrDefault(u => u.UserName == userName);
                var tag = db.Tags.FirstOrDefault(t => t.TagName == tagName);

                user.Tags.Remove(tag);

                db.SaveChanges();

                Console.WriteLine("Deleted tag: " + tagName + " from user: " + userName + "\n");
            }
        }

        public void ReadUserTags()
        {
            using (var db = new BloggingContext())
            {
                var tags = from b in db.Tags
                    orderby b.TagName
                    select b;

                var users = from b in db.UserInformation
                    orderby b.UserName
                    select b;

                //Console.WriteLine("All tags in the database:");
                foreach (var t in tags)
                {
                    foreach (var u in users)
                    {
                        Console.WriteLine("User: " + u.UserName + "\t tag: " + t.TagName);
                    }
                }
                Console.WriteLine("\n");
            }
        }
    }
}
