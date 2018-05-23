using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;
using Prism.Mvvm;

namespace MartUI.Me
{
    // Inspiration from:
    // https://stackoverflow.com/questions/23188507/mvvm-shared-properties

    public class MyData : BindableBase
    {
        private static MyData _instance;

        protected MyData()
        {

        }

        // Return an instance (singleton!)
        public static MyData GetInstance()
        {
            return _instance ?? (_instance = new MyData());
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        // This is not good but needed to not constantly ask server for validation - should be done in actual product
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private List<string> _tags;
        public List<string> Tags
        {
            get => _tags ?? (_tags = new List<string>());
            set
            {
                if (_tags != value)
                {
                    _tags.Clear();
                    foreach (var tag in value)
                    {
                        _tags.Add(tag);
                    }
                    RaisePropertyChanged();
                }
            } 
        }

        private Uri _image;
        public Uri Image
        {
            get => _image ?? (_image = new Uri("pack://application:,,,/Images/ProfilePicPlaceholder.PNG"));
            set => SetProperty(ref _image, value);
        }

        private string _description;
        public string Description
        {
            get => _description ?? (_description = "Describe yourself!");
            set => SetProperty(ref _description, value);
        }
    }
}
