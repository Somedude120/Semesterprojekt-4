﻿using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MartUI.Events;
using MartUI.Friend;
using MartUI.Main;
using MartUI.Me;
using MartUI.Settings.BlankSetting;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Profile
{
    class ProfileViewModel : BindableBase, IViewModel
    {
        private readonly IEventAggregator _eventAggregator = GetEventAggregator.Get();
        private MyData _userData;
        private ICommand _return;
        private ICommand _saveChanges;
        private ICommand _chooseProfilePicture;
        private bool _otherUser;
        private string _username;
        private Uri _image;

        // When set, will not be able to change information
        public bool OtherUser
        {
            get => _otherUser;
            set => SetProperty(ref _otherUser, value);
        }

        public string ReferenceName => "Profile";
        public MyData UserData => _userData ?? (_userData = MyData.GetInstance());

        public Uri Image
        {
            get => _image ?? (_image = new Uri("pack://application:,,,/Images/ProfilePicPlaceholder.PNG"));
            set => SetProperty(ref _image, value);
        }

        public string Username
        {
            get => _username ?? (_username = UserData.Username);
            set => SetProperty(ref _username, value);
        }
        // Only change this variable to not override UserData yet
        private string _description;
        public string Description
        {
            get => _description ?? (_description = UserData.Description);
            set => SetProperty(ref _description, value);
        }

        private string _tags;
        public string Tags
        {
            get => _tags ?? (_tags = UserTagsInOneString);
            set => SetProperty(ref _tags, value);
        }

        private string _userTagsInOneString;
        private string UserTagsInOneString
        {
            get => _userTagsInOneString ?? (_userTagsInOneString = ConvertTagsToString(','));
            set => _userTagsInOneString = value;
        }

        // Converts the tags to one string separated by commas
        private string ConvertTagsToString(char delimiter)
        {
            if (UserData.Tags.Any())
            {
                StringBuilder str = new StringBuilder();

                for (int i = 0; i < UserData.Tags.Count - 1; i++)
                    str.Append(UserData.Tags[i] + delimiter);

                str.Append(UserData.Tags[UserData.Tags.Count - 1]);

                return str.ToString();
            }

            return "";
        }

        // Update tags of UserData
        private void UpdateTags()
        {
            UserData.Tags.Clear();

            var tagsFromUser = Tags.Split(',');

            foreach (var tag in tagsFromUser)
            {
                UserData.Tags.Add(tag);
            }
        }

        public ProfileViewModel()
        {
            _eventAggregator.GetEvent<ShowOtherUserProfile>().Subscribe(ShowFriendProfile);
            _eventAggregator.GetEvent<ReturnToProfile>().Subscribe(ToMyProfile);
        }

        private void ShowFriendProfile(string username)
        {
            OtherUser = true;
            _eventAggregator.GetEvent<GetProfile>().Subscribe(Profile);

            _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(Constants.RequestProfile + 
                                                                          Constants.GroupDelimiter + 
                                                                          username);
        }

        private void Profile(string profile)
        {
            //MessageBox.Show(profile);
            _eventAggregator.GetEvent<ChangeFocusPage>().Publish(new ProfileViewModel());

            var fullProfile = profile.Split(Constants.GroupDelimiter).ToList();

            Username = fullProfile[0];

            if (fullProfile.Count > 1)
                Description = fullProfile[1];

            if (fullProfile.Count > 2)
            {
                var tags = fullProfile[2].Split(Constants.DataDelimiter).ToList();

                StringBuilder prettyTags = new StringBuilder();

                for (int i = 0; i < tags.Count - 1; i++)
                {
                    prettyTags.Append(tags[i] + ',');
                }

                prettyTags.Append(tags[tags.Count - 1]);

                Tags = prettyTags.ToString();
            }
        }

        public ICommand Return => _return ?? (_return = new DelegateCommand(ShowFriends));

        public ICommand SaveChanges => _saveChanges ?? (_saveChanges = new DelegateCommand(Save));
        public ICommand ChooseProfilePicture => _chooseProfilePicture ?? (_chooseProfilePicture = new DelegateCommand(ChoosePicture));

        private void ChoosePicture()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                UserData.Image = new Uri(dialog.FileName);
                Image = UserData.Image;
            }
        }

        public void ToMyProfile()
        {
            Username = UserData.Username;
            Description = UserData.Description;
            Tags = ConvertTagsToString(',');

            OtherUser = false;
        }

        private void Save()
        {
            UpdateTags();
            UserTagsInOneString = ConvertTagsToString(',');
            UserData.Description = Description;

            var tagsToSend = ConvertTagsToString(Constants.DataDelimiter);

            // Send profile to server
            var msg = Constants.UpdateProfile + Constants.GroupDelimiter + 
                      UserData.Username + Constants.GroupDelimiter + UserData.Description
                      + Constants.GroupDelimiter + tagsToSend;

            _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg);
        }

        // Will update based on user input (YES/NO/CANCEL)
        public void ShowFriends()
        {
            if (Username == UserData.Username)
            {

                if (Tags != UserTagsInOneString || Description != UserData.Description)
                {
                    var result = MessageBox.Show("You have made changes that are not saved. Save?", "Confirmation",
                        MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Cancel)
                        return;

                    if (result == MessageBoxResult.Yes)
                    {
                        Save();
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        Tags = UserTagsInOneString;
                        Description = UserData.Description;
                    }
                }
            }

            _eventAggregator.GetEvent<ChangeFriendPage>().Publish(new FriendViewModel());
            _eventAggregator.GetEvent<ChangeFocusPage>().Publish(new BlankSettingViewModel());

           ToMyProfile();
        }
    }
}
