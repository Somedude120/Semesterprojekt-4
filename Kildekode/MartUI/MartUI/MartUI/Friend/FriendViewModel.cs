using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MartUI.Chat;
using MartUI.Events;
using MartUI.Main;
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
        public ICommand ChooseFriendCommand { get; set; }
        //public ICommand AddFriendCommand;
        //public ICommand RemoveFriendCommand;
        private ICommand _addFriendCommand;
        private ICommand _removeFriendCommand;
        private ICommand _doThreadedWorkCommand;


        public FriendViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();

            //FriendList = new List<FriendModel>();
            ChooseFriendCommand = new DelegateCommand<FriendModel>(SelectFriend);

            // Mulig løsning til når venner logger ind:
            // Subscribe på et event som serveren sender så man kan se når en ven logger ind

            for (int i = 0; i < 3; i++)
            {
                var friend = new FriendModel {Username = "Friend" + i};
                FriendList.Add(friend);
            }

            //Tilføj eventuelt et eller andet som første plads i arrayet
            //Skal bruge metode fra server/database til at få en liste af alle ens venner
            //Samt kun alle som er online 
        }

        public ICommand DoThreadedWorkCommand => _doThreadedWorkCommand ?? (_doThreadedWorkCommand = new DelegateCommand(doSomething));

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
            set => SetProperty(ref _friendList, value);
        }

        public FriendModel SelectedFriend
        {
            get => _selectedFriend;
            set => SetProperty(ref _selectedFriend, value);
        }

        private void SelectFriend(FriendModel friend)
        {
            _eventAggregator.GetEvent<SelectedFriendEvent>().Publish(friend);
            _eventAggregator.GetEvent<ChangeFocusPage>().Publish(new ChatViewModel());
        }

        public void AddFriend()
        {
            for(int i = 0; i < 20; i++)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    FriendList.Add(new FriendModel {Username = "Yee" + i});
                }));
            }
            //if (!FriendList.Contains(_selectedFriend))
            //    FriendList.Add(_selectedFriend);
            //else
            //    MessageBox.Show("This user is already on your friendlist!");

            //Skal kommunikere med database/server
        }

        public void RemoveFriend()
        {
            if (FriendList.Contains(SelectedFriend))
            {
                FriendList.Remove(SelectedFriend);
            }
            else
                MessageBox.Show("This user is not on your friendlist!");

            //Skal kommunikere med database/server
            //Lav dropdown når der højreklikkes på en ven hvorved muligheden for at fjerne mm. fremvises
        }

        public ICommand AddFriendCommand => _addFriendCommand ?? (_addFriendCommand = new DelegateCommand(AddFriend));
        public ICommand RemoveFriendCommand => _removeFriendCommand ?? (_removeFriendCommand = new DelegateCommand(RemoveFriend));

        private void doSomething()
        {
            Thread myThread = new Thread(AddFriend);
            myThread.Start();
        }

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
