using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private readonly IEventAggregator _eventAggregator = GetEventAggregator.Get();

        private DatabaseDummy _database;
        private DetailedPersonModel _detailedPerson;

        private ICommand _registerButton;
        private ICommand _backButton;

        // Gå i gang med noget onpropertychanged perhaps
        public DetailedPersonModel Person
        {
            get
            {
                if(_detailedPerson == null)
                    _detailedPerson = new DetailedPersonModel();
                return _detailedPerson;
            }
            set
            {
            MessageBox.Show("seetting");
                SetProperty(ref _detailedPerson, value);

            }
        }

        // instead implement with Onpropertychanged or smth else


        public CreateUserViewModel()
        {
            Person.Name = "hej";
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
        public ICommand BackButton
        {
            get
            {
                if (_backButton == null)
                    _backButton = new DelegateCommand(() =>
                        _eventAggregator.GetEvent<ChangeFullPage>().Publish(new LoginViewModel()));
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
    }
}
