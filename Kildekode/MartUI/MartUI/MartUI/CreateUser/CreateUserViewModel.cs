using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using MartUI.Events;
using MartUI.Helpers;
using MartUI.Login;
using MartUI.Main;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.CreateUser
{
    public class CreateUserViewModel : BindableBase, IViewModel
    {
        public string ReferenceName => "CreateUser";

        private readonly IEventAggregator _eventAggregator;

        private DatabaseDummy _database;

        private ICommand _registerButton;
        private ICommand _backButton;


        public CreateUserViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _database = new DatabaseDummy();

            _database.PersonList.Add(new PersonModel("hajsa12", "goodpass1"));
            _database.PersonList.Add(new PersonModel("coolguy", "coolpass"));

        }

        public ICommand BackButton
        {
            get
            {
                if (_backButton == null)
                    _backButton = new DelegateCommand(() =>
                        _eventAggregator.GetEvent<ChangeFullPage>().Publish(new LoginViewModel(_eventAggregator)));
                return _backButton;
            }
        }

        public ICommand RegisterButton
        {
            get 
            {
                if (_registerButton == null)
                    _registerButton = new DelegateCommand(CreateNewUser);
                return _registerButton;
            }
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
