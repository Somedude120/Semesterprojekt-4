using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartUI.CreateUser
{
    public class DetailedPersonModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public List<string> Tags { get; set; }
        public Image Picture { get; set; }
    }
}
