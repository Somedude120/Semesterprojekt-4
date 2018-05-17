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
        private MyData _myData;
        private IEventAggregator _eventAggregator;
        private ICommand _sendMessageCommand;
        private string _textToSend;
        private ObservableCollection<FriendModel> _friendList;
        private FriendModel _user;

        public string ReferenceName => "ChatView";

        public MyData MyData => _myData ?? (_myData = MyData.GetInstance());

        public string TextToSend
        {
            get { return _textToSend; }
            set
            {
                _textToSend = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<FriendModel> FriendList
        {
            get
            {
                if(_friendList == null)
                    _friendList = new ObservableCollection<FriendModel>();
                return _friendList;
            }
            set { _friendList = value; }
        }

        public FriendModel User
        {
            get
            {
                if(_user == null)
                    _user = new FriendModel();
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }

        public ChatViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            _eventAggregator.GetEvent<SelectedFriendEvent>().Subscribe(HandleFriend);
            _eventAggregator.GetEvent<GetFriendListEvent>().Subscribe(HandleFriendList);

            User = new FriendModel();

            //Receive all previous messages
        }

        private void HandleFriendList(ObservableCollection<FriendModel> list)
        {
            FriendList.Clear();
            foreach (var friend in list)
            {
                FriendList.Add(friend);
            }
        }

        private void HandleFriend(FriendModel obj)
        {
            User = obj;
        }

        private void SendMessage()
        {
            //Send TextToSend + Username/Recipient
            var message = new ChatModel();
            message.Sender = MyData.Username;
            message.Message = TextToSend;
            message.MessagePosition = "Right";
            message.Receiver = User.Username;
            _eventAggregator.GetEvent<NewMessageEvent>().Publish(message);
            ReceiveMessage(TextToSend, MyData.Username, User.Username);
            ReceiveMessage(TextToSend, User.Username, MyData.Username);
            TextToSend = "";
        }

        private void ReceiveMessage(string TextToReceive, string Sender, string Receiver)
        {
            var message = new ChatModel();

            if (Sender == MyData.Username)
            {
                message.Sender = MyData.Username;
                message.Receiver = Receiver;
                message.Message = TextToReceive;
                message.MessagePosition = "Right";
            }
            else
            {
                message.Sender = Sender;
                message.Receiver = MyData.Username;
                message.Message = TextToReceive;
                message.MessagePosition = "Left";
            }
            _eventAggregator.GetEvent<NewMessageEvent>().Publish(message);
        }

        public ICommand SendMessageCommand => _sendMessageCommand ?? (_sendMessageCommand = new DelegateCommand(SendMessage));
    }
}
