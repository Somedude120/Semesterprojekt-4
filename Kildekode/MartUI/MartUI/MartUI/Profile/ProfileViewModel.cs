﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MartUI.Events;
using MartUI.Friend;
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

        public string ReferenceName => "Profile";
        public MyData UserData => _userData ?? (_userData = MyData.GetInstance());

        public ProfileViewModel()
        {
            UserData.Username = "Bent";
        }
        public ICommand Return => _return ?? (_return = new DelegateCommand(ShowFriends));

        public void ShowFriends()
        {
            _eventAggregator.GetEvent<ChangeFriendPage>().Publish(new FriendViewModel());
            _eventAggregator.GetEvent<ChangeFocusPage>().Publish(new BlankSettingViewModel());
        }
    }
}