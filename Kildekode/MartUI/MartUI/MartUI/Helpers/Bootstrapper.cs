using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace MartUI.Helpers
{
    class Bootstrapper : UnityBootstrapper
    {
        // Returns instance of MainWindow
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void InitializeShell()
        {
            if (Application.Current.MainWindow != null)
                Application.Current.MainWindow.Show();
        }
    }
}
