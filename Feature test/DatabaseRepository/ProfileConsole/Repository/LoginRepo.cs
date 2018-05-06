using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class LoginRepo
    {
        public void CreateLogin(string userName, string salt, string hash)
        {
            using (var db = new BloggingContext())
            {
                var login = new Login {UserName = userName, Salt = salt, Hash = hash};
                db.Login.Add(login);
                db.SaveChanges();

                Console.WriteLine("Added Login for user: " + login.UserName+ " to the database\n");
            }
        }

        //public void ReadLogin()
        //{
        //    using (var db = new BloggingContext())
        //    {
        //        var query = from b in db.Login
        //                    orderby b.UserName
        //                    select b;

        //        Console.WriteLine("All logins in the database:");
        //        foreach (var item in query)
        //        {
        //            Console.WriteLine(item.UserName);
        //        }
        //        Console.WriteLine("\n");
        //    }
        //}

        public void deleteLogin(string userName)
        {
            using (var db = new BloggingContext())
            {
                var DeleteLogin =
                    from p in db.Login
                    where p.UserName == userName
                    select p;

                foreach (var login in DeleteLogin)
                {
                    db.Login.Remove(login);
                    Console.WriteLine("Deleted login for user : " + login.UserName + " from the database\n");
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

        public void UpdateLogin(string userName, string newSalt, string newHash)
        {
            using (var db = new BloggingContext())
            {
                var deleteLogin =
                    from p in db.Login
                    where p.UserName == userName
                    select p;

                foreach (var login in deleteLogin)
                {
                    db.Login.Remove(login);
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

                var newLogin = new Login { UserName= userName, Salt= newSalt, Hash= newHash};
                db.Login.Add(newLogin);
                db.SaveChanges();
                Console.WriteLine("Updated salt/hash for user: " + userName + "\n");
            }
        }
    }
}
