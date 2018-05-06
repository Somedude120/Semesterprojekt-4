using System;
using System.Windows;
using System.Windows.Input;
using MartUI.Helpers;
using MartUI.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MartUI.Login
{
    class LoginViewModel : BindableBase // Using this from PRISM instead of INotifyPropertyChanged
    {

        private string _username;
        private string _password;
        private readonly IRegionManager _regionManager;
        public ICommand CreateUserCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private PersonModel _dataModel;

        private DatabaseDummy _database;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); } // if username != value, notify
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); } // if username != value, notify
        }

        

        public LoginViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _dataModel = new PersonModel();
            _username = _dataModel.Username;
            _password = _dataModel.Password;

            _database = new DatabaseDummy();
            _database.PersonList.Add(new PersonModel("hajsa12", "goodpass1"));
            _database.PersonList.Add(new PersonModel("coolguy", "coolpass"));

            //Delegates instead of the execute/cant execute
            //Observes Username and Password to check CanExecute, call RaiseCanExecute
            LoginCommand = new DelegateCommand(LoginExecute, LoginCanExecute).ObservesProperty(() => Username);
            CreateUserCommand = new DelegateCommand(CreateUser);
            // CanExecute behøver ikke være en metode men også en boolean property
        }

        private void CreateUser()
        {
            MessageBox.Show("hej");
            _regionManager.RequestNavigate("MainRegion", "CreateUserView");
        }

        private bool LoginCanExecute()
        {
            return !String.IsNullOrWhiteSpace(Username) && Username.Length > 4;
            //  && !String.IsNullOrWhiteSpace(Password) && Password.Length > 5
        }

        private void LoginExecute()
        {
            // PASTE INTO CREATE NEW USER VIEW/VIEWMODEL
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
