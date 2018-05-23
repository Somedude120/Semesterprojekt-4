using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MartUI.Events;
using MartUI.Friend;
using MartUI.Main;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Tag
{
    public class TagViewModel : BindableBase, IViewModel
    {
        public string ReferenceName => "TagViewModel";
        private IEventAggregator _eventAggregator;
        private ObservableCollection<string> _userList;
        private ICommand _addFriendFromTagCommand;
        private ICommand _showProfileCommand;
        private ICommand _searchTagCommand;
        public string Tag = "Enter tag!";

        public ICommand AddFriendFromTagCommand => _addFriendFromTagCommand ?? (_addFriendFromTagCommand= new DelegateCommand<string>(AddFriendFromTag));
        public ICommand ShowProfileCommand => _showProfileCommand ?? (_showProfileCommand = new DelegateCommand<string>(ShowProfile));
        public ICommand SearchTagCommand => _searchTagCommand ?? (_searchTagCommand = new DelegateCommand(SearchTag));

        public ObservableCollection<string> UserList
        {
            get
            {
                if (_userList == null)
                    _userList = new ObservableCollection<string>();
                return _userList;
            }
            set { _userList = value; }
        }

        public TagViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            _eventAggregator.GetEvent<GetTagEvent>().Subscribe(HandleGetTag);
        }

        private void SearchTag()
        {
            string message = Constants.GetUsernamesByTag
        }

        private void HandleGetTag(string username)
        {
            _userList.Add(username);
        }

        private void AddFriendFromTag(string username)
        {
            _eventAggregator.GetEvent<AddFriendFromTagEvent>().Publish(username);
        }

        private void ShowProfile(string username)
        {
            //_eventAggregator.GetEvent<ShowProfileEvent>().Publish(username);
        }
    }
}
