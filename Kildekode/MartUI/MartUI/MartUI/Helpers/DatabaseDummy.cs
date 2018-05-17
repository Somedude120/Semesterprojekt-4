using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using MartUI.CreateUser;
using MartUI.Login;
using MartUI.Me;

namespace MartUI.Helpers
{
    public class DatabaseDummy
    {
        private MyData _user;
        private MyData UserData => _user ?? (_user = MyData.GetInstance());

        private static DatabaseDummy _databaseDummy;
        public static DatabaseDummy GetInstance()
        {
            return _databaseDummy ?? (_databaseDummy = new DatabaseDummy());
        }

        private static List<DetailedPersonModel> _people;
        public List<DetailedPersonModel> People => _people ?? (_people = new List<DetailedPersonModel>());

        public bool UsernameExist(string newUsername)
        {
            foreach (var user in People)
            {
                if (user.Username == newUsername)
                    return true;
            }
            return false;
        }

        public void ValidateUser(string username)
        {
            if (UsernameExist(username))
            {
                Console.WriteLine("Validate password");

                //_databaseDummy.People

            }
        }

        public void Print()
        {
            Debug.WriteLine("\nDatabase consists of:");

            foreach (var user in People)
            {
                Debug.WriteLine("Username: " + user.Username + " Password: " + user.Password);
                if (user.Tags.Any())
                {
                    Debug.Write("Tags: ");

                    foreach (var userDataTag in user.Tags)
                    {
                        Debug.Write(userDataTag + ", ");
                    }
                }
                Debug.WriteLine("\nImagePath: " + user.Image.LocalPath);
            }
        }
    }
}
