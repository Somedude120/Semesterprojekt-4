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
            set
            {
                _friendRequests = value; 
                RaisePropertyChanged();
            }
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
        //public ICommand DeclineFriendRequestCommand => _declineFriendRequestCommand ?? (_declineFriendRequestCommand = new DelegateCommand(DeclineFriendRequest));
        //public ICommand BlockFriendRequestCommand => _blockFriendRequestCommand ?? (_blockFriendRequestCommand = new DelegateCommand(BlockFriendRequest));
        //public ICommand AcceptAllFriendRequestsCommand => _acceptAllFriendRequestsCommand ?? (_acceptAllFriendRequestsCommand = new DelegateCommand(AcceptFriendAllFriendRequest));
        //public ICommand DeclineAllFriendRequestsCommand => _declineAllFriendRequestsCommand ?? (_declineAllFriendRequestsCommand = new DelegateCommand(DeclineAllFriendRequests));
        //public ICommand CloseNotificationsCommand => _closeNotificationCommand ?? (_closeNotificationCommand = new DelegateCommand(CloseNotification));
        //public ICommand CloseAllNotificationsCommand => _closeAllNotificationsCommand ?? (_closeAllNotificationsCommand = new DelegateCommand(CloseAllNotifications));

        public FriendNotificationViewModel()
        {
            FriendNotifications.Add("Hejsa");
            FriendNotifications.Add("Hejsa");
            FriendRequests.Add("Hej");
            FriendRequests.Add("Hej");
        }

        private void AcceptFriendRequest(string username)
        {
            FriendRequests.Remove(username);
            var msg = Constants.AcceptFriendRequest + Constants.MiddleDelimiter + username;
            //Application.Current.Dispatcher.Invoke(() => { _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(msg); });
        }
    }
}
