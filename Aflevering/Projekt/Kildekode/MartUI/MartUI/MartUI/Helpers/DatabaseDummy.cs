using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MartUI.CreateUser;

namespace MartUI.Helpers
{
    public class DatabaseDummy
    {
        private static DatabaseDummy _databaseDummy;
        public static DatabaseDummy GetInstance()
        {
            return _databaseDummy ?? (_databaseDummy = new DatabaseDummy());
        }

        protected DatabaseDummy()
        {
            People.Add(new DetailedPersonModel
            {
                Username = "HeyMan",
                Password = "NeverGuessIt",
                Tags = new List<string> { "YePls", "FriendsPls" }
            });

            People.Add(new DetailedPersonModel
            {
                Username = "CoolGuy",
                Password = "hahamanIAmCool",
                Tags = new List<string> { "YePls", "NoPls" }
            });

            People.Add(new DetailedPersonModel
            {
                Username = "supman",
                Password = "niceman",
                Tags = new List<string> { "YePls", "NoPls" }
            });
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

        public bool ValidateUser(string username, string password)
        {
            var user = GetUser(username);

            if (user == null) return false;

            return user.Username == username && user.Password == password;
        }

        public DetailedPersonModel GetUser(string user)
        {
            foreach (var person in People)
            {
                if (person.Username == user)
                    return person;
            }

            return null;
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
