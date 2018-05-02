using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class UserInformationRepo
    {
        public void CreateUserInformation(string name, string userName)
        {
            using (var db = new BloggingContext())
            {
                var user = new UserInformation { Name = name, UserName = userName };
                db.UserInformation.Add(user);
                db.SaveChanges();

                Console.WriteLine("Tilføjede brugernavn: " + user.UserName + " til databasen\n");
            }
        }

        public void ReadUserInformation()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.UserInformation
                            orderby b.UserName
                            select b;

                Console.WriteLine("All users in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.UserName);
                }
                Console.WriteLine("\n");
            }
        }

        public void deleteUserInformation(string userName)
        {
            using (var db = new BloggingContext())
            {
                var DeleteUser =
                    from p in db.UserInformation
                    where p.UserName == userName
                    select p;

                foreach (var user in DeleteUser)
                {
                    db.UserInformation.Remove(user);
                    Console.WriteLine("Deleted user : " + user.UserName + " from the database\n");
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
