using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace MartUI.CreateUser
{
    public class DetailedPersonModel  : BindableBase
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> Tags { get; set; }
        public Image Picture { get; set; }
    }
}
