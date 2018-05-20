using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MartUI.Events;
using MartUI.Helpers;
using MartUI.Login;
using MartUI.Main;
using MartUI.Me;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.CreateUser
{
    public class CreateUserViewModel : BindableBase, IViewModel
    {
        private MyData _userData;
        private DatabaseDummy _databaseDummy;

        private readonly IEventAggregator _eventAggregator = GetEventAggregator.Get();

        private ICommand _registerButton;
        private ICommand _backButton;
        private ICommand _chooseProfilePicture;

        public string ReferenceName => "CreateUser";

        public MyData UserData => _userData ?? (_userData = MyData.GetInstance());
        public DatabaseDummy DatabaseDummy => _databaseDummy ?? (_databaseDummy = DatabaseDummy.GetInstance());

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

        // Need to do this to be able to observe password - cannot observe complex property
        public string Password
        {
            get => UserData.Password;
            set
            {
                if (UserData.Password == value) return;
                UserData.Password = value;
                RaisePropertyChanged();
            } 
        }

        public CreateUserViewModel()
        {
            //Subscriptions
            _eventAggregator.GetEvent<PasswordChangedInCreate>().Subscribe(para => Password = para);
            _eventAggregator.GetEvent<ChangingTagsInCreate>().Subscribe(ModifyTags);
        }

        // Will publish event of ChangeFullPage to LoginViewModel
        public ICommand BackButton => _backButton ?? (_backButton = new DelegateCommand(() =>
                                          _eventAggregator.GetEvent<ChangeFullPage>().Publish(new LoginViewModel())));

        // Will call CreateNewUser
        public ICommand RegisterButton => _registerButton ?? (_registerButton = new DelegateCommand(CreateNewUser, CanRegister)
                                          .ObservesProperty(() => Username)
                                          .ObservesProperty(() => Password));

        private bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(Username) && Username.Length > 4
                    && !string.IsNullOrWhiteSpace(Password) && Password.Length > 5;
        }

        public ICommand ChooseProfilePicture => _chooseProfilePicture ?? (_chooseProfilePicture = new DelegateCommand(ChoosePicture));

        private void ChoosePicture()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
                UserData.Image = new Uri(dialog.FileName);
        }

        private void CreateNewUser()
        {
            //THIS IS SERVER STUFF, ONLY FOR TESTING!!
            if (DatabaseDummy.UsernameExist(Username))
            {
                MessageBox.Show("Username " + Username + " already exists! Choose something else");
                // Change to something more pretty ..
            }
            else
            {
                Debug.WriteLine("Added " + Username);
                DatabaseDummy.People.Add(new DetailedPersonModel
                {
                    Username = Username,
                    Image = UserData.Image,
                    Password = Password,
                    Tags = UserData.Tags
                });

                // Change view to login
                _eventAggregator.GetEvent<ChangeFullPage>().Publish(new LoginViewModel());

                MessageBox.Show($"Welcome to the club, {UserData.Username}! You can now log in");
            }

            DatabaseDummy.Print();
        }

        public void ModifyTags(TagControl tag)
        {
            if (!tag.Command)
            {
                if (UserData.Tags.Any())
                    UserData.Tags.RemoveAt(UserData.Tags.Count - 1);
            }
            else
            {
                UserData.Tags.Add(tag.Tag);
            }
        }
    }
}
