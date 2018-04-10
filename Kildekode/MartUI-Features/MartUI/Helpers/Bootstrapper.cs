using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace MartUI
{
    // Initialize all services
    class Bootstrapper : UnityBootstrapper
    {
        //Returns instance of MainWindow 
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        //Need to override to be able to resolve types
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Register types
            Container.RegisterType(typeof(object), typeof(Login.Login), "Login");
            //Container.RegisterType(typeof(object), typeof(CreateUserView), "CreateUserView");
        }



    }
}
