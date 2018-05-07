using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MartUI.Events;
using MartUI.Focus;
using MartUI.Friend;
using MartUI.Login;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Reflection;

namespace MartUI.Main
{
    class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator = GetEventAggregator.Get();
        //IRegionManager regionManager = new RegionManager();

        private IViewModel _fullView;
        //private ICommand _changeView;
        private IViewModel _focusView;
        private IViewModel _sideBarView;
        private IViewModel _friendListView;

        public List<IViewModel> _viewList;


        public MainViewModel()
        {
            // Make one method that does these 
            _eventAggregator.GetEvent<ChangeFullPage>().Subscribe(ChangeFullView);
            _eventAggregator.GetEvent<ChangeFocusPage>().Subscribe(ChangeFocusView);
            _eventAggregator.GetEvent<ChangeFriendPage>().Subscribe(ChangeFriendView);
            _eventAggregator.GetEvent<ChangeSideBarPage>().Subscribe(ChangeSideBarView);

            //ViewList.Add(new LoginViewModel());
            ViewList.Add(new FriendViewModel());
            //ViewList.Add(new FocusViewModel(eventAggregator));

            FriendListView = ViewList[0];
            //FocusView = new FocusViewModel(eventAggregator);
            //FullView = ViewList[0];
            //regionManager.
            //regionManager.RequestNavigate("FullView", "LoginView");
        }


        // If equal null, return new, else return _viewList
        public List<IViewModel> ViewList => _viewList ?? (_viewList = new List<IViewModel>());

        public IViewModel FullView
        {
            get => _fullView;
            set => SetProperty(ref _fullView, value);
        }

        public IViewModel FocusView

        {
            get => _focusView;
            set => SetProperty(ref _focusView, value);
        }

        public IViewModel FriendListView
        {
            get => _friendListView;
            set => SetProperty(ref _friendListView, value);
        }

        public IViewModel SideBarView
        {
            get => _sideBarView;
            set => SetProperty(ref _sideBarView, value);
        }

        private void ChangeSideBarView(IViewModel model)
        {
            SideBarView = GetTrueModel(model);
        }

        private void ChangeFriendView(IViewModel model)
        {
            FriendListView = GetTrueModel(model);
        }

        private void ChangeFocusView(IViewModel model)
        {
            FocusView = GetTrueModel(model);
        }

        private void ChangeFullView(IViewModel model)
        {
            // Idk about this for-loop 
            FocusView = null;
            SideBarView = null;
            FriendListView = null;

            FullView = GetTrueModel(model);
        }

        // To prevent duplicates in the list when adding new views!! 
        private IViewModel GetTrueModel(IViewModel model)
        {
            bool isFound = false;

            foreach (var view in ViewList)
            {
                Console.WriteLine(ViewList.Count);
                if (model.ReferenceName == view.ReferenceName)
                {
                    isFound = true;
                    model = view;
                    break;
                }
            }

            if (!isFound)
                ViewList.Add(model);

            return model;
        }

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
