//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Input;
//using MartUI.Events;
//using MartUI.Main;
//using MartUI.Me;
//using Prism.Commands;
//using Prism.Events;
//using Prism.Mvvm;

//namespace MartUI.Group
//{
//    class GroupViewModel: BindableBase, IViewModel
//    {
//        private readonly IEventAggregator _eventAggregator;
//        public string ReferenceName => "GroupViewModel"; // Returns "FriendViewModel"
//        private ObservableCollection<GroupModel> _groupList;
//        private GroupModel _selectedGroup;
//        private ICommand _chooseGroupCommand;
//        private ICommand _addGroupCommand;
//        private ICommand _removeGroupCommand;
//        private string _groupName;
//        private MyData _userData;
//        public MyData UserData => _userData ?? (_userData = MyData.GetInstance());

//        private bool _notificionReceived;
//        public string GroupName
//        {
//            get { return _groupName; }
//            set
//            {
//                _groupName = value;
//                RaisePropertyChanged();
//            }
//        }

//        //public Brush Background => NotificationReceived ? Brushes.White : Brushes.Red;
//        public ICommand ChooseGroupCommand => _chooseGroupCommand?? (_chooseGroupCommand = new DelegateCommand<GroupModel>(SelectGroup));

//        public GroupViewModel()
//        {
//            GroupName = "Enter Groupname!";

//            _eventAggregator.GetEvent<ReceiveMessageFromServerEvent>().Subscribe(HandleNewMessage);
//            _eventAggregator.GetEvent<NewGroupMessageEvent>().Subscribe(HandleNewMessage);
//            _eventAggregator.GetEvent<AcceptedGroupRequestEvent>().Subscribe(AcceptedGroupRequest);
//            _eventAggregator.GetEvent<RemoveGroupReceivedEvent>().Subscribe(HandleRemoveGroupReceived);

//            // Mulig løsning til når venner logger ind:
//            // Subscribe på et event som serveren sender så man kan se når en ven logger ind

//            var marto = new GroupModel() { GroupName = "Marto" };
//            var alexD = new GroupModel() { GroupName = "AlexD" };
//            GroupList.Add(marto);
//            GroupList.Add(alexD);

//            //Tilføj eventuelt et eller andet som første plads i arrayet
//            //Skal bruge metode fra server/database til at få en liste af alle ens venner
//            //Samt kun alle som er online 
//        }

//        //private void ReceivedNotification()
//        //{

//        //}


//        //private void HandleNewMessage(ChatModel message)
//        //{
//        //    foreach (var group in GroupList)
//        //    {
//        //        if (message.Sender == UserData.Username && group.Username == message.Receiver)
//        //        {
//        //            message.MessagePosition = "Right";
//        //            group.MessageList.Add(message);
//        //            break;
//        //        }
//        //        else if (message.Sender == group.Username && UserData.Username == message.Receiver)
//        //        {
//        //            message.MessagePosition = "Left";
//        //            friend.MessageList.Add(message);
//        //            break;
//        //        }
//        //    }
//        //}

//        // Mangler at tilføje en filtrering eller en anden liste som kun indeholder online venner

//        // All friends in friend list
//        public ObservableCollection<GroupModel> GroupList
//        {
//            get
//            {
//                if (_groupList == null)
//                    _groupList = new ObservableCollection<GroupModel>();
//                return _groupList;
//            }
//            set
//            {
//                _groupList = value;
//                RaisePropertyChanged();
//            }
//        }

//        public GroupModel SelectedGroup
//        {
//            get => _selectedGroup;
//            set => SetProperty(ref _selectedGroup, value);
//        }

//        private void SelectGroup(GroupModel group)
//        {

//            _eventAggregator.GetEvent<SelectedGroupEvent>().Publish(group);
//            _eventAggregator.GetEvent<ChangeFocusPage>().Publish(group.Chat);
//        }

//        public void AddGroup()
//        {
//            var groupInList = false;
//            foreach (var g in GroupList)
//            {
//                if (g.GroupName == GroupName)
//                {
//                    MessageBox.Show("This group is already on your grouplist");
//                    groupInList = true;
//                    break;
//                }
//            }

//            if (!groupInList)
//            {
//                var message = Constants.SendGroupRequest + Constants.MiddleDelimiter + GroupName;
//                _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(message);
//            }

//            GroupName = ""; //Clears the AddFriendTextbox after pressing enter
//            //Skal kommunikere med database/server
//        }

//        public void AcceptedGroupRequest(string groupName)
//        {
//            var groupInList = false;
//            foreach (var g in GroupList)
//            {
//                if (g.GroupName == groupName)
//                {
//                    MessageBox.Show("This group is already on your grouplist");
//                    groupInList = true;
//                    break;
//                }
//            }
//            if (!groupInList)
//            {
//                Application.Current.Dispatcher.Invoke(() => { GroupList.Add(new GroupModel() { GroupName = groupName }); });
//            }
//        }

//        public void RemoveGroup(GroupModel group)
//        {
//            if (GroupList.Contains(group))
//            {
//                GroupList.Remove(group);
//                var message = Constants.RemoveGroup + Constants.MiddleDelimiter + GroupName;
//                _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(message);
//            }
//            else
//                MessageBox.Show("This user is not on your grouplist!");

//            //Skal kommunikere med database/server
//        }

//        public void HandleRemoveGroupReceived(string groupName)
//        {
//            var isInList = false;
//            var group = new GroupModel();
//            foreach (var g in GroupList)
//            {
//                if (g.GroupName == groupName)
//                {
//                    isInList = true;
//                    group = g;
//                }
//            }

//            if (isInList)
//            {
//                var message = group.GroupLeader + " has removed you from their group!";
//                GroupList.Remove(group);
//                _eventAggregator.GetEvent<NotificationReceivedEvent>().Publish(message);
//            }
//        }

//        public ICommand AddGroupCommand => _addGroupCommand ?? (_addGroupCommand = new DelegateCommand(AddGroup));
//        public ICommand RemoveGroupCommand => _removeGroupCommand ?? (_removeGroupCommand = new DelegateCommand<GroupModel>(RemoveGroup));

//    }

//    public class BindingProxy : Freezable
//    {
//        protected override Freezable CreateInstanceCore()
//        {
//            return new Friend.BindingProxy();
//        }

//        public object Data
//        {
//            get { return (object)GetValue(DataProperty); }
//            set { SetValue(DataProperty, value); }
//        }

//        public static readonly DependencyProperty DataProperty =
//            DependencyProperty.Register("Data", typeof(object), typeof(Friend.BindingProxy), new UIPropertyMetadata(null));
//    }
//}
