using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace MartUI.Settings
{
    class SettingsViewModel : BindableBase, IViewModel
    {
        public string ReferenceName
        {
            get { return "Settings"; }
        }
    }
}
