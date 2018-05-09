using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartUI.Chat;
using MartUI.Events;
using MartUI.Main;
using MartUI.Settings.BlankSetting;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Settings
{
    class SettingsViewModel : BindableBase, IViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        public string ReferenceName => "Settings";

        public SettingsViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
            _eventAggregator.GetEvent<ChangeFocusPage>().Publish(new BlankSettingViewModel());
        }
    }
}
