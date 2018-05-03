using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MartUI.Login;
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

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Register types
            //Container.RegisterType(typeof(object), typeof(LoginView), "LoginView");
            //Container.RegisterType(typeof(object), typeof(MainView), "MainView");
            // Container.RegisterType(typeof(object), typeof(DebitView), "DebitView");
        }
    }
}
