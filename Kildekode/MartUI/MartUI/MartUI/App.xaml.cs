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

            //SslTcpClient client = new SslTcpClient();
            //Thread clientThread = new Thread(client.ReceiveMessages);
            //clientThread.Start();

            var bs = new Bootstrapper();

            bs.Run();
        }
    }
}
