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

        // Made these in here since it will be created either way because observing these - no need for model
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


        public LoginViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            _eventAggregator.GetEvent<PasswordChangedInLogin>().Subscribe(paraPass => Password = paraPass);

            //Observes Username and Password to check LoginCanExecute, call LoginExecute
            LoginCommand = new DelegateCommand(LoginExecute, LoginCanExecute).ObservesProperty(() => Username).ObservesProperty(() => Password);

            // Navigate to CreateUserView
            CreateUserCommand = new DelegateCommand(() => _eventAggregator.GetEvent<ChangeFullPage>().Publish(new CreateUserViewModel()));
        }

        private bool LoginCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Username) && Username.Length > 4
                    && !string.IsNullOrWhiteSpace(Password) && Password.Length > 5;
        }

        private void LoginExecute()
        {
            // Validate name and password with server
            // Navigate to main window (friend list shows, etc).


            // TESTS THAT CASTS EXCEPTION ON INCORRECT INFO
            //bool correct = false;

            //try
            //{
            //    if (!correct)
            //        throw new Exception();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}

        }
    }
}
