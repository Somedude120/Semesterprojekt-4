using System.Collections.Generic;
using System.Collections.ObjectModel;
using MartUI.Chat;
using Prism.Mvvm;

namespace MartUI.Friend
{
    public class FriendModel : BindableBase
    {
        private string _username;
        private ObservableCollection<ChatModel> _messageList;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
            }
        }

        public ObservableCollection<ChatModel> MessageList
        {
            get
            {
                if(_messageList == null)
                    _messageList = new ObservableCollection<ChatModel>();
                return _messageList;
            }
            set
            {
                _messageList = value;
            }
        }
    }
}
