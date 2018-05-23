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
            _eventAggregator.GetEvent<SendMessageToServerEvent>().Subscribe(SendMessage);
            //Server subscription
            //Here the new event should be
            //_eventAggregator.GetEvent<SendMessageToServerEvent>().Subscribe(SendMessage);

            string machineName = "192.168.101.1";
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

        // Send messages to server
        public void SendMessage(string message)
        {
            sender.SendString(sslStream, message);
        }

        //// Receive messages from server - handle by publishing events to GUI
        ////public void ReceiveMessages()
        //{
        //    while (true)
        //    {
        //        string tempString = receiver.ReceiveString(sslStream);
        //        string[] tempStringList = tempString.Split(Constants.GroupDelimiter);
        //        if (tempStringList[0] == Constants.MessageReceived)
        //        {
        //            var message = new ChatModel();
        //            message.Message = tempStringList[2];
        //            message.Sender = tempStringList[1];
        //            message.Receiver = UserData.Username;
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                _eventAggregator.GetEvent<ReceiveMessageFromServerEvent>().Publish(message);
        //            });
        //        }
        //        //Receive friendrequest
        //        else if (tempStringList[0] == Constants.FriendRequestReceived)
        //        {
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                _eventAggregator.GetEvent<FriendRequestReceivedEvent>().Publish(tempStringList[1]);
        //            });
        //        }
        //        //Delete/Remove friend
        //        else if (tempStringList[0] == Constants.RemoveFriendReceived)
        //        {
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                _eventAggregator.GetEvent<RemoveFriendReceivedEvent>().Publish(tempStringList[1]);
        //            });
        //        }
        //        else if (tempStringList[0] == Constants.NotificationReceived)
        //        {
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                _eventAggregator.GetEvent<NotificationReceivedEvent>().Publish(tempStringList[1]);
        //            });
        //        }
        //        else if (tempStringList[0] == Constants.LoginResponse)
        //        {
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                _eventAggregator.GetEvent<LoginResponseEvent>().Publish(tempStringList[1]);
        //            });
        //        }
        //        else if (tempStringList[0] == Constants.Signup)
        //        {
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                _eventAggregator.GetEvent<SignupResponseEvent>().Publish(tempStringList[1]);
        //            });
        //        }
        //        else if (tempStringList[0] == Constants.FriendRequestDeclined)
        //        {
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                _eventAggregator.GetEvent<FriendRequestDeclinedEvent>().Publish(tempStringList[1]);
        //            });
        //        }
        //    }
        //}

        public void ReceiveMessages()
        {
            string tempString = receiver.ReceiveString(sslStream);
            string[] tempStringList = tempString.Split(Constants.GroupDelimiter);

            //MessageBox.Show(tempStringList[0]);
            switch (tempStringList[0])
            {
                case Constants.MessageReceived:
                    var message = new ChatModel();
                    message.Message = tempStringList[2];
                    message.Sender = tempStringList[1];
                    message.Receiver = UserData.Username;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<ReceiveMessageFromServerEvent>().Publish(message);
                    });
                    break;
                case Constants.FriendRequestReceived:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<FriendRequestReceivedEvent>().Publish(tempStringList[1]);
                    });
                    break;
                case Constants.RemoveFriendReceived:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<RemoveFriendReceivedEvent>().Publish(tempStringList[1]);
                    });
                    break;
                case Constants.NotificationReceived:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<NotificationReceivedEvent>().Publish(tempStringList[1]);
                    });
                    break;
                case "OK":
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<LoginResponseEvent>().Publish("OK");
                    });
                    break;
                case "NOK":
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<LoginResponseEvent>().Publish("NOK");
                    });
                    break;
                case Constants.GetProfile:
                    //If it receives ok from server, get profile
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<GetProfile>().Publish(tempStringList[1] + Constants.GroupDelimiter +  tempStringList[2]);

                    });
                    break;
                case Constants.GetFriendList:
                    //If received Profile
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        //If Ok, receive friendlist
                        _eventAggregator.GetEvent<GetFriendList>().Publish(tempStringList[1]);

                    });
                    break;
                case Constants.Signup:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<SignupResponseEvent>().Publish(tempStringList[1]);
                    });
                    break;
                case Constants.FriendRequestDeclined:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<FriendRequestDeclinedEvent>().Publish(tempStringList[1]);
                    });
                    break;
                case Constants.GetUsernamesByTag:
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _eventAggregator.GetEvent<GetTagEvent>().Publish(tempStringList[1]);
                    });
                    break;
                default:
                    Console.WriteLine($"Error Messagehandler: {tempStringList[0]}");
                    break;
            }
        }


        //public void MessageHandler(string message)
        //{
        //    string[] temp = message.Split(Constants.GroupDelimiter);
        //    switch (temp[0])
        //    {
        //        case "W":
        //            //Write message
        //            SendMessage(message);
        //            break;
        //        case "L":
        //            //Login
        //            Login(message);
        //            break;
        //        case "FR":
        //            //To a friend request message (In receive)
        //            SendFriendRequest(message);
        //            break;
        //        case "RF":
        //            SendRemoveFriend(message);
        //            break;
        //        case "AFR":
        //            AcceptFriendRequest(message);
        //            break;
        //        case "":
        //            SendRemoveFriend(message);
        //            break;
        //        case "":
        //            SendRemoveFriend(message);
        //            break;
        //        case "":
        //            SendRemoveFriend(message);
        //            break;

        //        default:
        //            Console.WriteLine($"Debugging: {message[0]} : {message}");
        //            break;
        //    }
        //}

        //public void Login(string userName)
        //{

        //    string myString = "W" + Constants.GroupDelimiter + message.Receiver + Constants.GroupDelimiter + message.Message;
        //    sender.SendString(sslStream, myString);

        //    sender.SendString(sslStream,userName);
        //}
        //public void Login(string userName)
        //{
        //    //string myString = "W" + Constants.GroupDelimiter + message.Receiver + Constants.GroupDelimiter + message.Message;

        //    //sender.SendString(sslStream,userName);
        //    sender.SendString(sslStream, userName);
        //}


        //public void SendFriendRequest(string fRequest)
        //{
        //    sender.SendString(sslStream, fRequest);
        //}
        //public void SendRemoveFriend(string rFriend)
        //{
        //    sender.SendString(sslStream, rFriend);
        //}
        //public void ReceiveMessage(string guiMessage)
        //{
        //    ChatModel message;
        //    while (true)
        //    {
        //        guiMessage = receiver.ReceiveString(sslStream);
        //        //It should split with middle delimiters
        //        string[] tempStringList = guiMessage.Split(';');
        //        message = new ChatModel();
        //        message.Sender = tempStringList[2];
        //        message.Sender = tempStringList[1];
        //        message.Receiver = UserData.Username;
        //        Application.Current.Dispatcher.Invoke(() =>
        //        {
        //            _eventAggregator.GetEvent<ReceiveMessageFromServerEvent>().Publish(message);
        //        });
        //    }
        //}

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





        //private static void DisplayUsage()
        //{
        //    Console.WriteLine("To start the client specify:");
        //    Console.WriteLine("clientSync machineName [serverName]");
        //    Environment.Exit(1);
        //}

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