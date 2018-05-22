using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MartUI.Events;
using MartUI.Main;
using MartUI.Me;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.FriendNotification
{
    class FriendNotificationViewModel : BindableBase, IViewModel
    {
        private IEventAggregator _eventAggregator;
        private MyData _userData;
        public MyData UserData => _userData ?? (_userData = MyData.GetInstance());
        public string ReferenceName => "FriendNotification";
        private ObservableCollection<string> _friendRequests;
        private ObservableCollection<string> _friendNotifications;
        private ICommand _acceptFriendRequestCommand;
        private ICommand _declineFriendRequestCommand;
        private ICommand _blockFriendRequestCommand;
        private ICommand _acceptAllFriendRequestsCommand;
        private ICommand _declineAllFriendRequestsCommand;
        private ICommand _closeNotificationCommand;
        private ICommand _closeAllNotificationsCommand;

        public ObservableCollection<string> FriendRequests
        {
            get
            {
                if(_friendRequests == null)
                    _friendRequests = new ObservableCollection<string>();
                return _friendRequests;
            }
            set { _friendRequests = value; }
        }

        public ObservableCollection<string> FriendNotifications
        {
            get
            {
                if (_friendNotifications == null)
                    _friendNotifications = new ObservableCollection<string>();
                return _friendNotifications;
            }
            set { _friendNotifications = value; }
        }

        public ICommand AcceptFriendRequestCommand => _acceptFriendRequestCommand ?? (_acceptFriendRequestCommand = new DelegateCommand<string>(AcceptFriendRequest));
        public ICommand DeclineFriendRequestCommand => _declineFriendRequestCommand ?? (_declineFriendRequestCommand = new DelegateCommand<string>(DeclineFriendRequest));
        public ICommand BlockFriendRequestCommand => _blockFriendRequestCommand ?? (_blockFriendRequestCommand = new DelegateCommand<string>(BlockFriendRequest));
        public ICommand AcceptAllFriendRequestsCommand => _acceptAllFriendRequestsCommand ?? (_acceptAllFriendRequestsCommand = new DelegateCommand(AcceptAllFriendRequests));
        public ICommand DeclineAllFriendRequestsCommand => _declineAllFriendRequestsCommand ?? (_declineAllFriendRequestsCommand = new DelegateCommand(DeclineAllFriendRequests));
        public ICommand CloseNotificationCommand => _closeNotificationCommand ?? (_closeNotificationCommand = new DelegateCommand<string>(CloseNotification));
        public ICommand CloseAllNotificationsCommand => _closeAllNotificationsCommand ?? (_closeAllNotificationsCommand = new DelegateCommand(CloseAllNotifications));

        public FriendNotificationViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            _eventAggregator.GetEvent<NotificationReceivedEvent>().Subscribe(HandleNotificationReceived);
            _eventAggregator.GetEvent<FriendRequestReceivedEvent>().Subscribe(HandleFriendRequestReceived);
            FriendNotifications.Add("Hejsa");
            FriendNotifications.Add("Hejsa");
            FriendRequests.Add("Hej");
            FriendRequests.Add("Hej");
        }

        private void AcceptFriendRequest(string username)
        {
            FriendRequests.Remove(username);
            var msg = Constants.AcceptFriendRequest + Constants.MiddleDelimiter + username;
            Application.Current.Dispatcher.Invoke(() => { _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg); });
            _eventAggregator.GetEvent<AcceptedFriendRequestEvent>().Publish(username);
        }

        private void DeclineFriendRequest(string username)
        {
            FriendRequests.Remove(username);
            var msg = Constants.DeclineFriendRequest + Constants.MiddleDelimiter + username;
            Application.Current.Dispatcher.Invoke(() => { _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg); });
        }

        private void BlockFriendRequest(string username)
        {
            FriendRequests.Remove(username);
            var msg = Constants.BlockFriendRequest + Constants.MiddleDelimiter + username;
            Application.Current.Dispatcher.Invoke(() => { _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg); });
        }

        private void AcceptAllFriendRequests()
        {
            foreach (var username in FriendRequests)
            {
                var msg = Constants.AcceptFriendRequest + Constants.MiddleDelimiter + username;
                Application.Current.Dispatcher.Invoke(() => { _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg); });
            }

            FriendRequests.Clear();
        }

        private void DeclineAllFriendRequests()
        {
            foreach (var username in FriendRequests)
            {
                var msg = Constants.DeclineFriendRequest + Constants.MiddleDelimiter + username;
                Application.Current.Dispatcher.Invoke(() => { _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg); });
            }
            FriendRequests.Clear();
        }

        private void CloseNotification(string notification)
        {
            FriendNotifications.Remove(notification);
            var msg = Constants.RemoveNotification + Constants.MiddleDelimiter + notification;
            Application.Current.Dispatcher.Invoke(() => { _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg); });
        }

        private void CloseAllNotifications()
        {
            foreach (var notification in FriendRequests)
            {
                var msg = Constants.RemoveNotification + Constants.MiddleDelimiter + notification;
                Application.Current.Dispatcher.Invoke(() => { _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg); });
            }
            FriendNotifications.Clear();
        }

        private void HandleNotificationReceived(string notification)
        {
            bool isNotInList = true;
            foreach (var n in FriendNotifications)
                if (n == notification)
                    isNotInList = false;
            if(isNotInList)
                FriendNotifications.Add(notification);
        }

        private void HandleFriendRequestReceived(string username)
        {
            bool isNotInList = true;
            foreach (var u in FriendRequests)
                if (u == username)
                    isNotInList = false;
            if (isNotInList)
                FriendRequests.Add(username);
        }
    }
}
