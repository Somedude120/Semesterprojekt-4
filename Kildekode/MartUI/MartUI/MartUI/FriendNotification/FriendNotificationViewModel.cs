using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MartUI.Events;
using MartUI.Main;
using Prism.Commands;
using Prism.Mvvm;

namespace MartUI.FriendNotification
{
    class FriendNotificationViewModel : BindableBase, IViewModel
    {
        public string ReferenceName => "FriendNotification";

        public ICommand AcceptAll { get; set; }

        public FriendNotificationViewModel()
        {
            AcceptAll = new DelegateCommand(() =>
                GetEventAggregator.Get().GetEvent<NotificationReceivedEvent>().Publish());
        }
    }
}
