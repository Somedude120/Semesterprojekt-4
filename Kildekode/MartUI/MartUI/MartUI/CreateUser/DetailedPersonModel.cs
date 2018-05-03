using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace MartUI.CreateUser
{
    public class DetailedPerson  
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> Tags { get; set; }
        public Image Picture { get; set; }
    }

    public class DetailedPersonModel : BindableBase
    {
        private DetailedPerson _person = new DetailedPerson();

        public string Name
        {
            get => _person.Name;
            set
            {
                _person.Name = value;
                RaisePropertyChanged();
            }
        }
    }
}
