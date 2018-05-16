using System;
using System.Collections.Generic;

namespace MartUI.Me
{
    public static class MyData
    {
        private static List<string> _tags;

        private static Uri _image;

        public static string Username { get; set; }
        public static string Password { get; set; }

        public static List<string> Tags
        {
            get => _tags ?? (_tags = new List<string>());
            set => _tags = value;
        }

        public static Uri Image
        {
            get => _image ?? new Uri("pack://application:,,,/Images/ProfilePicPlaceholder.PNG");
            set => _image = value;
        }
    }
    //public static class DetailedPersonModel
    //{
    //    //private DetailedPerson _person = new DetailedPerson();

    //    public static string Username
    //    {
    //        get => Username;
    //        set
    //        {
    //            if (Username == value) return;
    //            _person.Username = value;
    //        }
    //    }

    //    public string Password
    //    {
    //        get => _person.Password;
    //        set
    //        {
    //            if (_person.Password == value) return;
    //            _person.Password = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    public List<string> Tags
    //    {
    //        get => _person.Tags ?? (_person.Tags = new List<string>());
    //        set
    //        {
    //            if (_person.Tags == value) return;
    //            _person.Tags = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    public Uri Image
    //    {
    //        get => _person.Image ?? new Uri("pack://application:,,,/Images/ProfilePicPlaceholder.PNG");
    //        set
    //        {
    //            if (_person.Image == value) return;
    //            _person.Image = value;
    //            RaisePropertyChanged();
    //        }
    //    }
    //}
}
