using System;
using System.Collections.Generic;
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

namespace MartUI.Settings.BlankSetting
{
    class BlankSettingViewModel : BindableBase, IViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        public string ReferenceName => "BlankSetting";
        private ICommand _return;

        public BlankSettingViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
        }

        public ICommand Return
        {
            get
            {
                return _return ?? (_return = new DelegateCommand(() =>
                           _eventAggregator.GetEvent<ChangeFriendPage>().Publish(new FriendViewModel())));
            }
        }

    }
}
