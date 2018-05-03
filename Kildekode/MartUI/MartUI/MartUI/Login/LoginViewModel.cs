using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MartUI.CreateUser;
using MartUI.Events;
using MartUI.Main;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Login
{
    public class LoginViewModel : BindableBase, IViewModel // Using BindableBase from PRISM instead of INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        public string ReferenceName => "Login";

        private string _username;
        private string _password;

        public ICommand CreateUserCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private PersonModel _dataModel;
        //private DatabaseDummy _database;

        //public PersonModel Person
        //{
        //    get { MessageBox.Show("getter");return _dataModel; }
        //    set { SetProperty(ref _dataModel, value); } // if username != value, notify
        //}

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); } // if username != value, notify
        }

        //public string Password
        //{
        //    get { return _password; }
        //    set { SetProperty(ref _password, value); } // if username != value, notify
        //}

        public LoginViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            _dataModel = new PersonModel();
            _username = _dataModel.Username;
           // _password = _dataModel.Password;

            //var data = this.
            //_database = new DatabaseDummy();
            //_database.PersonList.Add(new PersonModel("hajsa12", "goodpass1"));
            //_database.PersonList.Add(new PersonModel("coolguy", "coolpass"));

            //Delegates instead of the execute/cant execute
            //Observes Username and Password to check CanExecute, call RaiseCanExecute
            LoginCommand = new DelegateCommand(LoginExecute, LoginCanExecute).ObservesProperty(() => Username);

            CreateUserCommand = new DelegateCommand(CreateUser);
            // CanExecute behøver ikke være en metode men også en boolean property
        }

        

        private void CreateUser()
        {
            _eventAggregator.GetEvent<ChangeFullPage>().Publish(new CreateUserViewModel());
            // NAVIGATE TO CREATE USER VIEW 
        }

        private bool LoginCanExecute()
        {
            //MessageBox.Show()
            // Username length  to be above 4 and pass above 5
            return !String.IsNullOrWhiteSpace(Username) && Username.Length > 4;
            //  && !String.IsNullOrWhiteSpace(Password) && Password.Length > 5
        }

        private void LoginExecute()
        {
            // Validate name and password with server
            // Navigate to main window (friend list shows, etc).
        }
    }
}
