using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartUI.Events;
using MartUI.Friend;
using MartUI.Main;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Chat
{
    public class ChatViewModel : BindableBase, IViewModel
    {
        private IEventAggregator _eventAggregator;
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        public string ReferenceName => "FocusView";

        public ChatViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            _eventAggregator.GetEvent<SelectedFriendEvent>().Subscribe(HandleFriend);
        }

        private void HandleFriend(FriendModel obj)
        {
            Username = obj.Username;
        }
    }
}
