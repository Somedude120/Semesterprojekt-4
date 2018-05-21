using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using MartUI.Chat;
using MartUI.Events;
using MartUI.Main;
using MartUI.Me;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Friend
{
    public class FriendViewModel : BindableBase, IViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        public string ReferenceName => "FriendViewModel"; // Returns "FriendViewModel"
        private ObservableCollection<FriendModel> _friendList;
        private FriendModel _selectedFriend;
        private ICommand _chooseFriendCommand;
        private ICommand _addFriendCommand;
        private ICommand _removeFriendCommand;
        private ICommand _showNotificationsCommand;
        private string _username;
        private MyData _userData;
        public MyData UserData => _userData ?? (_userData = MyData.GetInstance());


        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ChooseFriendCommand => _chooseFriendCommand ?? (_chooseFriendCommand = new DelegateCommand<FriendModel>(SelectFriend));

        public FriendViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();

            Username = "Enter Username!";

            _eventAggregator.GetEvent<ReceiveMessageFromServerEvent>().Subscribe(HandleNewMessage);
            _eventAggregator.GetEvent<NewMessageEvent>().Subscribe(HandleNewMessage);

            // Mulig løsning til når venner logger ind:
            // Subscribe på et event som serveren sender så man kan se når en ven logger ind

            var marto = new FriendModel() { Username = "Marto" };
            var alexD = new FriendModel() { Username = "AlexD" };
            FriendList.Add(marto);
            FriendList.Add(alexD);

            //Tilføj eventuelt et eller andet som første plads i arrayet
            //Skal bruge metode fra server/database til at få en liste af alle ens venner
            //Samt kun alle som er online 
        }

        private void HandleNewMessage(ChatModel message)
        {
            foreach (var friend in FriendList)
            {
                if (message.Sender == UserData.Username && friend.Username == message.Receiver)
                {
                    message.MessagePosition = "Right";
                    friend.MessageList.Add(message);
                    break;
                }
                else if (message.Sender == friend.Username && UserData.Username == message.Receiver)
                {
                    message.MessagePosition = "Left";
                    friend.MessageList.Add(message);
                    break;
                }
            }
        }

        // Mangler at tilføje en filtrering eller en anden liste som kun indeholder online venner

        // All friends in friend list
        public ObservableCollection<FriendModel> FriendList
        {
            get
            {
                if (_friendList == null)
                    _friendList = new ObservableCollection<FriendModel>();
                return _friendList;
            }
            set
            {
                _friendList = value;
                RaisePropertyChanged();
            }
        }

        public FriendModel SelectedFriend
        {
            get => _selectedFriend;
            set => SetProperty(ref _selectedFriend, value);
        }

        private void SelectFriend(FriendModel friend)
        {

            _eventAggregator.GetEvent<SelectedFriendEvent>().Publish(friend);
            _eventAggregator.GetEvent<ChangeFocusPage>().Publish(friend.Chat);
        }

        public void AddFriend()
        {
            bool friendInList = false;
            foreach (var f in FriendList)
            {
                if (f.Username == Username)
                {
                    MessageBox.Show("This user is already on your friendlist");
                    friendInList = true;
                }
            }

            if (!friendInList)
            {
                Application.Current.Dispatcher.Invoke(() => { FriendList.Add(new FriendModel {Username = Username}); });
            }

            Username = ""; //Clears the AddFriendTextbox after pressing enter
            //Skal kommunikere med database/server
        }

        public void RemoveFriend(FriendModel friend)
        {
            if (FriendList.Contains(friend))
            {
                FriendList.Remove(friend);
            }
            else
                MessageBox.Show("This user is not on your friendlist!");

            //Skal kommunikere med database/server
        }

        public ICommand ShowNotificationsCommand => _showNotificationsCommand ??
                                                    (_showNotificationsCommand = new DelegateCommand(
                                                        _eventAggregator.GetEvent<ChangeFocusPage>()
                                                            .Publish(new FriendNotificationViewModel())));
        public ICommand AddFriendCommand => _addFriendCommand ?? (_addFriendCommand = new DelegateCommand(AddFriend));
        public ICommand RemoveFriendCommand => _removeFriendCommand ?? (_removeFriendCommand = new DelegateCommand<FriendModel>(RemoveFriend));
    }

    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    }
}
