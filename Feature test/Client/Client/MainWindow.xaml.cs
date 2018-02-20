using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Windows.Interop;

//Code adapted from: http://csharp.net-informations.com/communications/csharp-client-socket.htm
namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            msg("Client Started");
            clientSocket.Connect("Localhost", 8888);
            msg("Client connected");
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            var serverStream = clientSocket.GetStream();
            var outStream = System.Text.Encoding.ASCII.GetBytes(sendTextBox.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            var inStream = new byte[10025];
            serverStream.Read(inStream, 0, inStream.Length);
            var returnData = System.Text.Encoding.ASCII.GetString(inStream);
            returnData = returnData.Substring(0, returnData.IndexOf("$"));
            msg(returnData);
        }

        public void msg(string mesg)
        {
            receiveTextBox.Text = receiveTextBox.Text + Environment.NewLine + mesg;
        }
    }
}
