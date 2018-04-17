using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace MartUI.Friend
{
    public class FriendViewModel : BindableBase, IViewModel
    {
        public string ReferenceName
        {
            get { return "FriendViewModel"; }
        }

        private List<IFriendModel> _friendList;
        private IFriendModel _selectedFriend;
        private ICommand _changeFriend;

        public FriendViewModel()
        {
            for (int i = 0; i < 41; i++)
            {
                var friend = new FriendModel();
                friend.UserName = "Friend" + i.ToString();
                friend.UserID = (100 + i).ToString();
                FriendList.Add(friend);
            }

            //Tilføj eventuelt et eller andet som første plads i arrayet
            //Skal bruge metode fra server/database til at få en liste af alle ens venner
        }

        public List<IFriendModel> FriendList
        {
            get
            {
                if (_friendList == null)
                    _friendList = new List<IFriendModel>();
                return _friendList;
            }
        }

        public IFriendModel SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                if (_selectedFriend != value)
                {
                    _selectedFriend = value;
                    RaisePropertyChanged("SelectedFriend");
                }
            }
        }

        private void ChangeSelectedFriend(IFriendModel friend)
        {
            if(!FriendList.Contains(friend))
                FriendList.Add(friend);
            SelectedFriend = FriendList.FirstOrDefault(f => f == friend);
        }

        public ICommand ChangeFriend
        {
            get
            {
                if (_changeFriend == null)
                {
                    _changeFriend = new DelegateCommand<IFriendModel>(f => ChangeSelectedFriend((IFriendModel)f), f => f is IFriendModel);
                }

                return _changeFriend;
            }
        }

        public void AddFriend(IFriendModel friend)
        {
            if (!FriendList.Contains(friend))
                FriendList.Add(friend);
            else
                MessageBox.Show("This user is already on your friendlist!");

            //Skal kommunikere med database/server
        }

        public void RemoveFriend(FriendModel friend)
        {
            if (FriendList.Contains(friend))
                FriendList.Remove(friend);
            else
                MessageBox.Show("This user is not on your friendlist!");

            //Skal kommunikere med database/server
        }
    }
}
