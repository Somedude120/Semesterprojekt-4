using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.Win32;
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
        private DetailedPersonModel _person;

        private ICommand _registerButton;
        private ICommand _backButton;
        private ICommand _chooseProfilePicture;

        private Image _image;
        private string _imagePath;
        private Uri _imageSource;

        public string Username
        {
            get => Person.Username;
            set
            {
                if (Person.Username == value) return;
                Person.Username = value;
                RaisePropertyChanged();
            } // if username != value, notify
        }

        // Need to do this to be able to observe password - cannot observe complex property
        public string Password
        {
            get => Person.Password;
            set
            {
                if (Person.Password == value) return;
                Person.Password = value;
                RaisePropertyChanged();
            } 
        }

        public CreateUserViewModel()
        {
            //Subscriptions
            _eventAggregator.GetEvent<PasswordChangedInCreate>().Subscribe(para => Password = para);
            _eventAggregator.GetEvent<ChangingTagsInCreate>().Subscribe(ModifyTags);

            _database = new DatabaseDummy();

            _database.PersonList.Add(new PersonModel("hajsa12", "goodpass1"));
            _database.PersonList.Add(new PersonModel("coolguy", "coolpass"));
        }

        public DetailedPersonModel Person => _person ?? (_person = new DetailedPersonModel());

        // Will publish event of ChangeFullPage to LoginViewModel
        public ICommand BackButton
        {
            get
            {
                return _backButton ?? (_backButton = new DelegateCommand(() =>
                           _eventAggregator.GetEvent<ChangeFullPage>().Publish(new LoginViewModel())));
            }
        }

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
            OpenFileDialog dialog = new OpenFileDialog {Title = "Select a picture"};

            if (dialog.ShowDialog() == true)
                Person.Image = new Uri(dialog.FileName);
        }

        private void CreateNewUser()
        {
            MessageBox.Show("username: " + Person.Username);
            MessageBox.Show("password: " + Person.Password);

            StringBuilder tags = new StringBuilder();

            foreach (var personTag in Person.Tags)
            {
                tags.Append(personTag + ", ");
            }

            MessageBox.Show(tags.ToString());


            // THIS IS SERVER STUFF, ONLY FOR TESTING!!
            //if (UsernameAlreadyExist(Username))
            //{
            //    MessageBox.Show("Username " + Username + " already exists! Choose something else");
            //    // Change to something more pretty ..
            //}
            //else
            //{
            //    Console.WriteLine("Added " + Username);
            //    _database.PersonList.Add(new PersonModel(Username, Password));
            //    // Change view
            //}

            //Console.WriteLine("\nDatabase consists of:");

            //foreach (var user in _database.PersonList)
            //{
            //    Console.WriteLine("Username: " + user.Username + " Password: " + user.Password);
            //}
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

        public void ModifyTags(TagControl tag)
        {
            if (!tag.Command)
            {
                if (Person.Tags.Any())
                    Person.Tags.RemoveAt(Person.Tags.Count - 1);
            }
            else
            {
                Person.Tags.Add(tag.Tag);
            }
        }
    }
}
