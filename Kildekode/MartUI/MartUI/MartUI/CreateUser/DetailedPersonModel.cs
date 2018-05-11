using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MartUI.Events;
using Prism.Commands;
using Prism.Mvvm;

namespace MartUI.CreateUser
{
    public class DetailedPerson  
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Tags { get; set; }
        public Uri Image { get; set; }
    }

    public class DetailedPersonModel : BindableBase
    {
        private DetailedPerson _person = new DetailedPerson();

        public string Username
        {
            get => _person.Username;
            set
            {
                if (_person.Username == value) return;
                _person.Username = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get => _person.Password;
            set
            {
                if (_person.Password == value) return;
                _person.Password = value;
                RaisePropertyChanged();
            }
        }

        public List<string> Tags
        {
            get => _person.Tags ?? (_person.Tags = new List<string>());
            set
            {
                if (_person.Tags == value) return;
                _person.Tags = value;
                RaisePropertyChanged();
            }
        }

        public Uri Image
        {
            get => _person.Image ?? new Uri("pack://application:,,,/Images/ProfilePicPlaceholder.PNG");
            set
            {
                if (_person.Image == value) return;
                _person.Image = value;
                RaisePropertyChanged();
            }
        }
    }
}
