using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MartUI.Helpers;

namespace MartUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // Override startup and run with boostrapper
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            //{
            //    var viewName = viewType.FullName;
            //    var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            //    var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}",
            //        viewName, viewAssemblyName);
            //    return Type.GetType(viewModelName);
            //});

            var bs = new Bootstrapper();

            bs.Run();
        }
    }
}
