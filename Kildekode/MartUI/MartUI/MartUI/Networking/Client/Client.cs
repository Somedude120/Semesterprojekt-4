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
        }

        // Send messages to server
        public void SendMessage(string message)
        {
            Sender.SendString(sslStream, message);
        }

        public void ReceiveMessages()
        {
            while (true)
            {
                string tempString = Receiver.ReceiveString(sslStream);
                string[] tempStringList = tempString.Split(Constants.GroupDelimiter);

                switch (tempStringList[0])
                {
                    case Constants.MessageReceived:
                        var message = new ChatModel();
                        message.Message = tempStringList[3];
                        message.Sender = tempStringList[1];
                        message.Receiver = tempStringList[2];

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
                            _eventAggregator.GetEvent<GetProfile>().Publish(tempStringList[1] + Constants.GroupDelimiter +  
                                                                            tempStringList[2] + Constants.GroupDelimiter + 
                                                                            tempStringList[3]);

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
                    case "SOK":
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _eventAggregator.GetEvent<SignupResponseEvent>().Publish("SOK");
                        });
                        break;
                    case "SNOK":
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _eventAggregator.GetEvent<SignupResponseEvent>().Publish("SNOK");
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
                    case Constants.FriendRequestAccepted:
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _eventAggregator.GetEvent<AcceptedFriendRequestEvent>().Publish(tempStringList[1]);
                        });
                        break;
                    default:
                        Console.WriteLine($"Error Messagehandler: {tempStringList[0]}");
                        break;
                }
            }
        }
    }
}