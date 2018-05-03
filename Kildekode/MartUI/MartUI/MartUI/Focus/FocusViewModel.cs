using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartUI.Events;
using MartUI.Friend;
using MartUI.Main;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Focus
{
    public class FocusViewModel : BindableBase, IViewModel
    {
        private IEventAggregator _eventAggregator;
        private string _username;
        public string Username 
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        public string ReferenceName => "FocusView";

        public FocusViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            Username = "test";
            _eventAggregator.GetEvent<SelectedFriendEvent>().Subscribe(HandleFriend);
        }

        private void HandleFriend(FriendModel obj)
        {
            Username = obj.Username;
        }
    }
}
