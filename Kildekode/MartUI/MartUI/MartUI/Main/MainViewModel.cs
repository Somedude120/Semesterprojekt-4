using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MartUI.Events;
using MartUI.Friend;
using MartUI.Login;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Reflection;
using Examples.System.Net;
using MartUI.Chat;
using MartUI.FriendNotification;
using MartUI.Group;
using MartUI.Me;
using MartUI.Profile;
using MartUI.Settings;
using MartUI.Settings.BlankSetting;

namespace MartUI.Main
{
    class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator = GetEventAggregator.Get();

        private IViewModel _fullView;
        private IViewModel _focusView;
        private IViewModel _sideBarView;
        private IViewModel _friendListView;
        private ICommand _settingsViewCommand;

        public List<IViewModel> _viewList;

        public MainViewModel()
        {
            // Make one method that does these 
            _eventAggregator.GetEvent<ChangeFullPage>().Subscribe(ChangeFullView);
            _eventAggregator.GetEvent<ChangeFocusPage>().Subscribe(ChangeFocusView);
            _eventAggregator.GetEvent<ChangeFriendPage>().Subscribe(ChangeFriendView);
            _eventAggregator.GetEvent<ChangeSideBarPage>().Subscribe(ChangeSideBarView);
            _eventAggregator.GetEvent<NotificationReceivedEvent>().Subscribe(Notify);
            _eventAggregator.GetEvent<DeleteProfileEvent>().Subscribe(ClearAll);

            ViewList.Add(new LoginViewModel());
            ViewList.Add(new FriendViewModel());
            ViewList.Add(new ChatViewModel());
            ViewList.Add(new ProfileViewModel());
            ViewList.Add(new FriendNotificationViewModel());
            ViewList.Add(new SettingsViewModel());

            FullView = ViewList[0];
        }


        private void ClearAll()
        {
            ViewList.Clear();

            ViewList.Add(new FriendViewModel());
            ViewList.Add(new ChatViewModel());
            ViewList.Add(new ProfileViewModel());
            ViewList.Add(new FriendNotificationViewModel());
            ViewList.Add(new SettingsViewModel());
        }

        private void Notify(string unused)
        {
            // Make sure not to notify if FocusView is FriendNotificationView
            if (FocusView.GetType().Name != "FriendNotificationViewModel" && unused == null)
                _eventAggregator.GetEvent<NotificationReceivedChangeColor>().Publish();
        }


        // If equal null, return new, else return _viewList
        public List<IViewModel> ViewList => _viewList ?? (_viewList = new List<IViewModel>());

        public IViewModel FullView
        {
            get => _fullView;
            set => SetProperty(ref _fullView, value);
        }

        public IViewModel FocusView

        {
            get => _focusView;
            set => SetProperty(ref _focusView, value);
        }

        public IViewModel FriendListView
        {
            get => _friendListView;
            set => SetProperty(ref _friendListView, value);
        }

        public IViewModel SideBarView
        {
            get => _sideBarView;
            set => SetProperty(ref _sideBarView, value);
        }

        private void ChangeSideBarView(IViewModel model)
        {
            SideBarView = GetTrueModel(model);
        }

        private void ChangeFriendView(IViewModel model)
        {
            FriendListView = GetTrueModel(model);
        }

        private void ChangeFocusView(IViewModel model)
        {
            FocusView = GetTrueModel(model);
        }

        private void ChangeFullView(IViewModel model)
        {
            // Idk about this for-loop 
            FocusView = null;
            SideBarView = null;
            FriendListView = null;

            FullView = GetTrueModel(model);
        }

        // To prevent duplicates in the list when adding new views!! 
        private IViewModel GetTrueModel(IViewModel model)
        {
            if (model == null) return null;

            bool isFound = false;

            foreach (var view in ViewList)
            {
                Console.WriteLine(ViewList.Count);
                if (model.ReferenceName == view.ReferenceName)
                {
                    isFound = true;
                    model = view;
                    //GC.Collect();
                    break;
                }
            }

            if (!isFound)
            {
                ViewList.Add(model);
            }


            return model;
        }

        public ICommand SettingsViewCommand => _settingsViewCommand ?? 
                                               (_settingsViewCommand = new DelegateCommand(ShowSettings));

        public void ShowSettings()
        {
            _eventAggregator.GetEvent<ReturnToProfile>().Publish();
            _eventAggregator.GetEvent<ChangeFriendPage>().Publish(new SettingsViewModel());
            _eventAggregator.GetEvent<ChangeFocusPage>().Publish(new ProfileViewModel());
        }
    }
}

