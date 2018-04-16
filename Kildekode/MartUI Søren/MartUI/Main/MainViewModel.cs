using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;
using MartUI.Friend;
using MartUI.Settings;
using Prism.Commands;
using Prism.Mvvm;

namespace MartUI.Main
{
    class MainViewModel : BindableBase
    {
        private ICommand _changeView;
        private IViewModel _currentView;
        private List<IViewModel> _viewList;

        public MainViewModel()
        {
            ViewList.Add(new FriendViewModel());
            ViewList.Add(new SettingsViewModel());

            CurrentView = ViewList[0];
        }

        public List<IViewModel> ViewList
        {
            get
            {
                if (_viewList == null)
                {
                    _viewList = new List<IViewModel>();
                }

                return _viewList;
            }
        }

        public IViewModel CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    RaisePropertyChanged("CurrentView");
                }
            }
        }

        private void ChangeViewModel(IViewModel viewModel)
        {
            if (!ViewList.Contains(viewModel))
                ViewList.Add(viewModel);
            CurrentView = ViewList.FirstOrDefault(vm => vm == viewModel);
        }

        public ICommand ChangeView
        {
            get
            {
                if (_changeView == null)
                {
                    _changeView = new DelegateCommand<IViewModel>(p => ChangeViewModel((IViewModel)p), p => p is IViewModel);
                }
                return _changeView;
            }
        }

        public ICommand SettingsViewClicked
        {
            get
            {
                if (_changeView == null && !(_currentView is SettingsViewModel))
                {
                    _changeView = new DelegateCommand<SettingsViewModel>(p => ChangeViewModel(new SettingsViewModel()));
                }
                return _changeView;
            }
        }

    }
}
