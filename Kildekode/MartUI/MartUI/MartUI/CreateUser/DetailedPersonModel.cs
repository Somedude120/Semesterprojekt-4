using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MartUI.Events;
using Prism.Mvvm;

namespace MartUI.CreateUser
{
    public class DetailedPerson  
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Tags { get; set; }
        public Image Picture { get; set; }
    }

    public class DetailedPersonModel : BindableBase
    {
        private DetailedPerson _person = new DetailedPerson();
        

        public string Username
        {
            get => _person.Username;
            set
            {
                _person.Username = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get => _person.Password;
            set
            {
                _person.Password = value;
                RaisePropertyChanged();
            }
        }

        public List<string> Tags
        {
            get => _person.Tags ?? (_person.Tags = new List<string>());
            set
            {
                _person.Tags = value;
                RaisePropertyChanged();
            }
        }

        public Image Picture
        {
            get => _person.Picture;
            set
            {
                _person.Picture = value;
                RaisePropertyChanged();
            }
        }
    }
}
