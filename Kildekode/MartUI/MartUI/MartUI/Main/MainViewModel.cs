using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MartUI.Focus;
using MartUI.Friend;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace MartUI.Main
{
    class MainViewModel : BindableBase
    {
        IEventAggregator eventAggregator = new EventAggregator();

        private ICommand _changeView;
        public List<IViewModel> _viewList;
        private IViewModel _friendListView;

        private IViewModel _focusView;
        public MainViewModel()
        {
            ViewList.Add(new FriendViewModel(eventAggregator));
            ViewList.Add(new FocusViewModel(eventAggregator));

            FriendListView = ViewList[0];
            FocusView = ViewList[1];
        }

        // If equal null, return new, else return _viewList
        public List<IViewModel> ViewList => _viewList ?? (_viewList = new List<IViewModel>());

        public IViewModel FriendListView
        {
            get => _friendListView;
            set => SetProperty(ref _friendListView, value);
        }

        public IViewModel FocusView
        {
            get => _focusView;
            set => SetProperty(ref _focusView, value);
        }

        //private void ChangeViewModel(IViewModel viewModel)
        //{
        //    if (!ViewList.Contains(viewModel))
        //        ViewList.Add(viewModel);
        //    FriendListView = ViewList.FirstOrDefault(vm => vm == viewModel);
        //}

        //public ICommand ChangeView
        //{
        //    get
        //    {
        //        if (_changeView == null)
        //        {
        //            _changeView = new DelegateCommand<IViewModel>(p => ChangeViewModel((IViewModel)p), p => p is IViewModel);
        //        }
        //        return _changeView;
        //    }
        //}
    }
}
