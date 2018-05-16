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
using MartUI.Me;
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
        //private string _addUsername;
        private string _textToSend;
        private ObservableCollection<ChatModel> _messageList;

        public string ReferenceName => "ChatView";

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        //public string AddUsername
        //{
        //    get => _addUsername;
        //    set => SetProperty(ref _addUsername, value);
        //}

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

            MyData.Username = "Me";

            for (int i = 0; i < 10; i++)
            {
                var message = new ChatModel();
                if (i % 2 == 0)
                {
                    message.Sender = MyData.Username;
                    message.Message = "Message" + i;
                    message.MessagePosition = "Left";
                }
                else
                {
                    message.Sender = "SomePerson" + i;
                    message.Message = "Message" + i;
                    message.MessagePosition = "Right";
                }
                MessageList.Add(message);
            }

            //Receive all previous messages
        }

        private void HandleFriend(FriendModel obj)
        {
            Username = obj.Username;
        }

        private void SendMessage()
        {
            //Send TextToSend + Username/Recipient
            var message = new ChatModel();
            message.Sender = MyData.Username;
            message.Message = TextToSend;
            message.MessagePosition = "Right";
            message.Receiver = Username;
            MessageList.Add(message);
        }

        private void ReceiveMessage()
        {
            
        }

        public ICommand SendMessageCommand => _sendMessageCommand ?? (_sendMessageCommand = new DelegateCommand(SendMessage));
    }
}
