using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MartUI.Chat;
using MartUI.Events;
using MartUI.Login;
using MartUI.Main;
using MartUI.Profile;
using MartUI.Settings.BlankSetting;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Settings
{
    class SettingsViewModel : BindableBase, IViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private ICommand _logoutCommand;
        public string ReferenceName => "Settings";

        public ICommand LogoutCommand => _logoutCommand ?? (_logoutCommand = new DelegateCommand(Logout));

        public SettingsViewModel()
        {
            _eventAggregator = GetEventAggregator.Get();
        }

        private void Logout()
        {
            _eventAggregator.GetEvent<LoginResponseEvent>().Publish("");
            _eventAggregator.GetEvent<SendMessageToServerEvent>().Publish(Constants.Logout);
            _eventAggregator.GetEvent<ChangeFullPage>().Publish(new LoginViewModel());
        }
    }
}
