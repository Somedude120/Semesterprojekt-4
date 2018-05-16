using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using MartUI.Events;
using MartUI.Friend;
using MartUI.Main;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Chat
{
    public class ChatViewModel : BindableBase, IViewModel
    {
        private IEventAggregator _eventAggregator;
        private ICommand _sendMessageCommand;
        private string _username;
        private string _textToSend;
        private ObservableCollection<ChatModel> _messageList;

        public string ReferenceName => "ChatView";

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ChatModel> MessageList
        {
            get
            {
                if (_messageList == null)
                    _messageList = new ObservableCollection<ChatModel>();
                return _messageList;
            }
            set => SetProperty(ref _messageList, value);
        }

        public ChatViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            _eventAggregator.GetEvent<SelectedFriendEvent>().Subscribe(HandleFriend);

            //for (int i = 0; i < 10; i++)
            //{
            //    var message = new ChatModel();
            //    if (i % 2 == 0)
            //    {
            //        message.Sender = 
            //    }
            //}

            //Receive all previous messages
        }

        private void HandleFriend(FriendModel obj)
        {
            Username = obj.Username;
        }

        private void SendMessage()
        {
            //Send TextToSend + Username/Recipient
            MessageBox.Show(TextToSend + " " + Username);
        }

        private void ReceiveMessage()
        {
            
        }

        public ICommand SendMessageCommand => _sendMessageCommand ?? (_sendMessageCommand = new DelegateCommand(SendMessage));
    }
}
