using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MartUI.Chat;
using MartUI.CreateUser;
using MartUI.Events;
using MartUI.Friend;
using MartUI.Helpers;
using MartUI.Main;
using MartUI.Me;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Login
{
    public class LoginViewModel : BindableBase, IViewModel // Using BindableBase from PRISM instead of INotifyPropertyChanged
    {
        private DatabaseDummy _databaseDummy;

        private readonly IEventAggregator _eventAggregator;

        private ICommand _createUserCommand;
        private ICommand _loginCommand;

        private string _password;
        private MyData _userData;

        public string ReferenceName => "Login";
        public MyData UserData => _userData ?? (_userData = MyData.GetInstance());
        public DatabaseDummy DatabaseDummy => _databaseDummy ?? (_databaseDummy = DatabaseDummy.GetInstance());

        // Made these in here since it will be created either way because observing these - no need for model
        public string Username
        {
            get => UserData.Username;
            set
            {
                if (UserData.Username == value) return;
                UserData.Username = value;
                RaisePropertyChanged();
            } // if username != value, notify
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
            // if username != value, notify
        }

        // Navigate to CreateUserView
        public ICommand CreateUserCommand => _createUserCommand ?? (_createUserCommand = new DelegateCommand(() =>
                                                     _eventAggregator.GetEvent<ChangeFullPage>().Publish(new CreateUserViewModel())));
        //Observes Username and Password to check LoginCanExecute, call LoginExecute
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand
                                            = new DelegateCommand(LoginExecute, LoginCanExecute)
                                                .ObservesProperty(() => Username)
                                                .ObservesProperty(() => Password));
        public LoginViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            _eventAggregator.GetEvent<PasswordChangedInLogin>().Subscribe(paraPass => Password = paraPass);
            _eventAggregator.GetEvent<LoginResponseEvent>().Subscribe(HandleLogin);
        }

        private void HandleLogin(string response)
        {
            switch (response)
            {
                case "OK":
                    // Fullpage is null, show friendlist and initialize chatview
                    _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish("RU");
                    _eventAggregator.GetEvent<GetProfile>().Subscribe(ProfileInfo);
                    _eventAggregator.GetEvent<GetFriendList>().Subscribe(FriendListInfo);


                    //_eventAggregator.GetEvent<ChangeFullPage>().Publish(null);
                    //_eventAggregator.GetEvent<ChangeFriendPage>().Publish(new FriendViewModel());
                    //_eventAggregator.GetEvent<ChangeFocusPage>().Publish(new ChatViewModel());
                    break;
                case "NOK":
                    MessageBox.Show("Wrong username or password!");
                    break;
            }
        }

        private void FriendListInfo(string s)
        {
            _eventAggregator.GetEvent<GetFriendListEvent>().Publish(s);
        }

        private void ProfileInfo(string profile)
        {
            var fullProfile = profile.Split(Constants.GroupDelimiter);

            UserData.Description = fullProfile[0];

            var tagsOnly = fullProfile[1].Split(Constants.DataDelimiter).ToList();

            UserData.Tags = tagsOnly;
        }

        private bool LoginCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Username) && Username.Length > 4
                    && !string.IsNullOrWhiteSpace(Password) && Password.Length > 5;
        }

        private void LoginExecute()
        {
            var msg = Constants.LoginResponse + Constants.GroupDelimiter + UserData.Username + Constants.GroupDelimiter + UserData.Password;

            _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg);



            // Validate name and password with server
            // Navigate to main window (friend list shows, etc).

            //if (DatabaseDummy.ValidateUser(Username, Password))
            //{
            //    // Fullpage is null, show friendlist and initialize chatview
            //    _eventAggregator.GetEvent<ChangeFullPage>().Publish(null);
            //    _eventAggregator.GetEvent<ChangeFriendPage>().Publish(new FriendViewModel());
            //    _eventAggregator.GetEvent<ChangeFocusPage>().Publish(new ChatViewModel());
            //}
            //else
            //    MessageBox.Show("Wrong username or password!");
        }
    }
}
