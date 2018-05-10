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


        private string ObserveUsername
        {
            get => Person.Username;
            //set;
        }


        public CreateUserViewModel()
        {

            //Subscriptions
            _eventAggregator.GetEvent<PasswordChangedInCreate>().Subscribe(SetPassword);
            _eventAggregator.GetEvent<ChangingTagsInCreate>().Subscribe(ModifyTags);

            _database = new DatabaseDummy();

            _database.PersonList.Add(new PersonModel("hajsa12", "goodpass1"));
            _database.PersonList.Add(new PersonModel("coolguy", "coolpass"));
        }

        public Uri ImageSource
        {
            get => _imageSource ?? new Uri("pack://application:,,,/Images/ProfilePicPlaceholder.PNG");
            set => SetProperty(ref _imageSource, value);
        }

        public DetailedPersonModel Person
        {
            get => _person ?? (_person = new DetailedPersonModel());
            //set => SetProperty(ref _person, value);
        }

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
        public ICommand RegisterButton =>
            _registerButton ?? (_registerButton = new DelegateCommand(CreateNewUser).ObservesCanExecute(() => HasCh));

        private bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(ObserveUsername);
            //&& !string.IsNullOrWhiteSpace(Person.Username) && Person.Password.Length > 5;
        }

        public ICommand ChooseProfilePicture => _chooseProfilePicture ?? (_chooseProfilePicture = new DelegateCommand(ChoosePicture));

        private void ChoosePicture()
        {
            OpenFileDialog dialog = new OpenFileDialog {Title = "Select a picture"};

            if (dialog.ShowDialog() == true)
                ImageSource = new Uri(dialog.FileName);
        }

        public void SetPassword(string pass)
        {
            Person.Password = pass;
        }
        private void CreateNewUser()
        {
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
