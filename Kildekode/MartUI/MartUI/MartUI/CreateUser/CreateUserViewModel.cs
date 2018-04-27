using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MartUI.Helpers;
using MartUI.Login;
using MartUI.Main;
using Prism.Mvvm;

namespace MartUI.CreateUser
{
    public class CreateUserViewModel : BindableBase, IViewModel
    {
        private DatabaseDummy _database;
        public string ReferenceName => "CreateUser";

        public CreateUserViewModel()
        {
            _database = new DatabaseDummy();

            _database.PersonList.Add(new PersonModel("hajsa12", "goodpass1"));
            _database.PersonList.Add(new PersonModel("coolguy", "coolpass"));

        }
        private void CreateNewUser()
        {
            string Username = "someName";
            string Password = "somepassword";

            // THIS IS SERVER STUFF, ONLY FOR TESTING!!
            if (UsernameAlreadyExist(Username))
            {
                MessageBox.Show("Username " + Username + " already exists! Choose something else");
                // Change to something more pretty ..
            }
            else
            {
                Console.WriteLine("Added " + Username);
                _database.PersonList.Add(new PersonModel(Username, Password));
                // Change view
            }

            Console.WriteLine("\nDatabase consists of:");

            foreach (var user in _database.PersonList)
            {
                Console.WriteLine("Username: " + user.Username + " Password: " + user.Password);
            }
        }

        private bool UsernameAlreadyExist(string newUsername)
        {
            foreach (var user in _database.PersonList)
            {
                if (user.Username == newUsername)
                    return true;
            }

            return false;
        }
    }

}
