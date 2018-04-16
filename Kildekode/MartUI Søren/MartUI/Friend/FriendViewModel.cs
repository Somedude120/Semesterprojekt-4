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
            get { return "Friend"; }
        }

        private List<FriendModel> _friendList;
        private FriendModel _selectedFriend;
        private ICommand _changeFriend;

        public FriendViewModel()
        {
            for (int i = 0; i < 21; i++)
            {
                var friend = new FriendModel();
                friend.UserName = "friend" + i.ToString();
                friend.UserID = (100 + i).ToString();
                FriendList.Add(friend);
            }
        }

        public List<FriendModel> FriendList
        {
            get
            {
                if (_friendList == null)
                    _friendList = new List<FriendModel>();
                return _friendList;
            }
        }

        public FriendModel SelectedFriend
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

        private void ChangeSelectedFriend(FriendModel friend)
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
                    _changeFriend = new DelegateCommand<FriendModel>(f => ChangeSelectedFriend((FriendModel)f), f => f is FriendModel);
                }

                return _changeFriend;
            }
        }

        public void AddFriend(FriendModel friend)
        {
            if (!FriendList.Contains(friend))
                FriendList.Add(friend);
            else
                MessageBox.Show(friend.UserName + " is already on your friendlist!");
        }

        public void RemoveFriend(FriendModel friend)
        {
            if (FriendList.Contains(friend))
                FriendList.Remove(friend);
            else
                MessageBox.Show(friend.UserName + " is not on your friendlist!");
        }
    }
}
