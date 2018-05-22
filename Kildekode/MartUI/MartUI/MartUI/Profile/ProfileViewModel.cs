using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MartUI.Events;
using MartUI.Friend;
using MartUI.Helpers;
using MartUI.Main;
using MartUI.Me;
using MartUI.Settings.BlankSetting;
using Prism.Commands;
using Prism.Events;

namespace MartUI.Profile
{
    class ProfileViewModel : IViewModel
    {
        private readonly IEventAggregator _eventAggregator = GetEventAggregator.Get();
        private MyData _userData;
        private ICommand _return;
        private ICommand _saveChanges;

        public string ReferenceName => "Profile";
        public MyData UserData => _userData ?? (_userData = MyData.GetInstance());

        // Only change this variable to not override UserData yet
        private string _tags;
        public string Tags
        {
            get => _tags ?? (_tags = UserTagsInOneString);
            set => _tags = value;
        }

        private string _userTagsInOneString;
        private string UserTagsInOneString
        {
            get => _userTagsInOneString ?? (_userTagsInOneString = ConvertTagsToString());
            set => _userTagsInOneString = value;
        }

        // Converts the tags to one string separated by commas
        private string ConvertTagsToString()
        {
            StringBuilder str = new StringBuilder();

            for(int i = 0; i < UserData.Tags.Count - 1; i++)
            {
                str.Append(UserData.Tags[i] + ",");
            }

           str.Append(UserData.Tags[UserData.Tags.Count - 1]);

            return str.ToString();
        }

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
            UserData.Username = "Hans";

        }
        public ICommand Return => _return ?? (_return = new DelegateCommand(ShowFriends));

        public ICommand SaveChanges => _saveChanges ?? (_saveChanges = new DelegateCommand(Save));

        private void Save()
        {
            UpdateTags();
            UserTagsInOneString = ConvertTagsToString();
        }

        public void ShowFriends()
        {
            if (Tags != UserTagsInOneString)
            {
                var result = MessageBox.Show("You have made changes that are not saved. Save?", "Confirmation",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Cancel)
                    return;

                if (result == MessageBoxResult.Yes)
                {
                    UpdateTags();
                    UserTagsInOneString = ConvertTagsToString();
                }
                else if (result == MessageBoxResult.No)
                {
                    Tags = UserTagsInOneString;
                }
            }

            _eventAggregator.GetEvent<ChangeFriendPage>().Publish(new FriendViewModel());
            _eventAggregator.GetEvent<ChangeFocusPage>().Publish(new BlankSettingViewModel());
        }
    }
}
