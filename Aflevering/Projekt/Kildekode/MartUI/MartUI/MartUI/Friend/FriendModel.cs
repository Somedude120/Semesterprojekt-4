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
        private ChatViewModel _chat;

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
                RaisePropertyChanged();
            }
        }

        public ChatViewModel Chat
        {
            get
            {
                if (_chat == null)
                    _chat = new ChatViewModel();
                return _chat;
            }
            set { _chat = value; }
        }
    }
}
