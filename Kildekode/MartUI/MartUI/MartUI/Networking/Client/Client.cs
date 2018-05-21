using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using MartUI.Chat;
using MartUI.Events;
using MartUI.Me;
using Prism.Events;
using TLSNetworking;

//Taken from: https://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2

namespace Examples.System.Net
{
    public class SslTcpClient
    {
        public static Receiver receiver = new Receiver();
        public static Sender sender = new Sender();
        private static SslStream sslStream;
        private readonly IEventAggregator _eventAggregator;
        private MyData _userData;

        public MyData UserData => _userData ?? (_userData = MyData.GetInstance());

        private static Hashtable certificateErrors = new Hashtable();

        public SslTcpClient()
        {
            //Get certain event from GUI
            _eventAggregator = GetEventAggregator.Get();
            //Server subscription
            //Here the new event should be
            //_eventAggregator.GetEvent<SendMessageToServerEvent>().Subscribe(SendMessage);

            string machineName = "192.168.137.1";
            string serverCertificateName = "Martin-MSI";
            SslTcpClient.RunClient(machineName, serverCertificateName);

            //From MyData
            //Login(UserData);
            //UserData.Username = "Hans";
            //string loginString = "L;" + UserData.Username;
            //sender.SendString(sslStream, loginString);

        }

        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            //X509ChainPolicy pol = new X509ChainPolicy()
            //{
            //    VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority | X509VerificationFlags.IgnoreCertificateAuthorityRevocationUnknown
            //                        | X509VerificationFlags.IgnoreCtlSignerRevocationUnknown | X509VerificationFlags.IgnoreEndRevocationUnknown
            //                        | X509VerificationFlags.IgnoreInvalidPolicy | X509VerificationFlags.IgnoreRootRevocationUnknown
            //                        | X509VerificationFlags.IgnoreWrongUsage | X509VerificationFlags.IgnoreInvalidName
            //};

            //chain.ChainPolicy = pol;
            
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            //return false;
            return true;    //Ignore false certificate. Used because of selfsigned certificate
        }
        public static void RunClient(string machineName, string serverName)
        {

            
            // Create a TCP/IP client socket.
            // machineName is the host running the server application.
            //TcpClient client = new TcpClient("192.168.173.1", 443);   //When client is not on localhost
            TcpClient client = new TcpClient(machineName, 443);
            //Console.WriteLine("Client connected.");
            // Create an SSL stream that will close the client's stream.

            sslStream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null
                );
            // The server name must match the name on the server certificate.
            try
            {
                Console.WriteLine(serverName);
                
                sslStream.AuthenticateAsClient(serverName);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                client.Close();
                return;
            }
            // Encode a test message into a byte array.
            // Signal the end of the message using the "<EOF>".
           // byte[] messsage;
            
            //Thread receiveThread = new Thread(o => ReceiveMessages((SslStream)o));
            //receiveThread.Start(sslStream);
            //ChatModel message = new ChatModel();
            //message.Message = "";
            //message.Receiver = "";
            //while (true)
            //{
            //    //messsage = Encoding.UTF8.GetBytes(Console.ReadLine() + Constants.EndDelimiter);
            //    //sslStream.Write(messsage);
            //    //sslStream.Flush();
            //    //sender.SendString(sslStream, SendMessage(message));
            //}

            //Console.ReadLine();
        }

        public void MessageHandler(string message)
        {
            //ChatModel Message = new ChatModel();
            switch (message[0])
            {
                case 'R':
                    //Read message
                    ReceiveMessage(message);
                    break;
                case 'W':
                    //Write message
                    SendMessage(message);
                    break;
                case 'L':
                    //Login
                    Login(message);
                    break;
                case 'A':
                    //To a friend request message (In receive)
                    break;
                default:
                    Console.WriteLine($"Debugging: {message[0]} : {message}");
                    break;

            }

        }

        public void Login(string userName)
        {
            sender.SendString(sslStream,userName);
        }

        public void SendMessage(string tempHans)
        {
            sender.SendString(sslStream,tempHans);
        }

        public void ReceiveMessage(string guiMessage)
        {
            ChatModel message;
            while (true)
            {
                guiMessage = receiver.ReceiveString(sslStream);
                //It should split with middle delimiters
                string[] tempStringList = guiMessage.Split(';');
                message = new ChatModel();
                message.Sender = tempStringList[2];
                message.Sender = tempStringList[1];
                message.Receiver = UserData.Username;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _eventAggregator.GetEvent<ReceiveMessageFromServerEvent>().Publish(message);
                });
            }

        }

        //public static void Login(MyData UserData)
        //{
        //    UserData.Username = "Daniel";
        //    //UserData.Username = UserName;
        //    string loginString = "L;" + UserData.Username;
        //    sender.SendString(sslStream, loginString);
        //}

        //public static void SendMessage(ChatModel message)
        //{
        //    string myString = "W;" + message.Receiver + ";" + message.Message;
        //    sender.SendString(sslStream, myString);
        //}



        //public void ReceiveMessages()
        //{
        //    ChatModel message;
        //    while (true)
        //    {
        //        string tempString = receiver.ReceiveString(sslStream);
        //        string[] tempStringList = tempString.Split(';');
        //        if (tempStringList[0] == "R")
        //        {
        //            message = new ChatModel();
        //            message.Message = tempStringList[2];
        //            message.Sender = tempStringList[1];
        //            message.Receiver = UserData.Username;
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                _eventAggregator.GetEvent<ReceiveMessageFromServerEvent>().Publish(message);
        //            });
        //        }
        //    }
        //}

        private static void DisplayUsage()
        {
            Console.WriteLine("To start the client specify:");
            Console.WriteLine("clientSync machineName [serverName]");
            Environment.Exit(1);
        }

        //public static int Main(string[] args)
        //{
        //    //string serverCertificateName = null;
        //    string serverCertificateName = "Martin-MSI";
        //    //string machineName = null;
        //    //string machineName = "Martin-MSI";
        //    //string machineName = "localhost";
        //    string machineName = "192.168.101.1";
        //    if (args == null || args.Length < 1)
        //    {
        //        //DisplayUsage();
        //    }
        //    // User can specify the machine name and server name.
        //    // Server name must match the name on the server's certificate. 
        //    //machineName = args[0];
        //    if (args.Length < 2)
        //    {
        //        //serverCertificateName = machineName;
        //    }
        //    else
        //    {
        //        serverCertificateName = args[1];
        //    }
        //    SslTcpClient.RunClient(machineName, serverCertificateName);
        //    return 0;
        //}
    }
}