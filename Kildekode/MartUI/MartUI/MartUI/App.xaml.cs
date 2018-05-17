using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Examples.System.Net;
using MartUI.Helpers;
using MartUI.Me;

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

            //MyData.Username = "Me";

            SslTcpClient client = new SslTcpClient();
            Thread Daniel = new Thread(client.ReceiveMessages);
            Daniel.Start();

            var bs = new Bootstrapper();

            bs.Run();
        }
    }
}
