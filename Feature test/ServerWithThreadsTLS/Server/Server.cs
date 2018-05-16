using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using TLSNetworking;

//Taken from: https://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2

namespace Examples.System.Net
{
    public sealed class SslTcpServer
    {
        public Receiver receiver = new Receiver();
        public Sender sender = new Sender();

        public static Dictionary<string, string> userID = new Dictionary<string, string>();
        public static Dictionary<string, SslStream> userStreams = new Dictionary<string, SslStream>();
        public static TcpListener listener = new TcpListener(IPAddress.Any, 443);
        static X509Certificate serverCertificate = null;
        // The certificate parameter specifies the name of the file 
        // containing the machine certificate.
        public static void RunServer(string certificate, TcpClient client)
        {
            serverCertificate = X509Certificate.CreateFromCertFile(certificate);

            ProcessClient(client);
        }
        static void ProcessClient(TcpClient client)
        {
            // A client has connected. Create the 
            // SslStream using the client's network stream.
            SslStream sslStream = new SslStream(
                client.GetStream(), false);
            // Authenticate the server but don't require the client to authenticate.
            try
            {
                sslStream.AuthenticateAsServer(serverCertificate,
                    false, SslProtocols.Tls, true);
                // Display the properties and settings for the authenticated stream.
                DisplaySecurityLevel(sslStream);
                DisplaySecurityServices(sslStream);
                DisplayCertificateInformation(sslStream);
                DisplayStreamProperties(sslStream);

                string IPId = ((IPEndPoint)client.Client.RemoteEndPoint).Address + "," + ((IPEndPoint)client.Client.RemoteEndPoint).Port;

                // Set timeouts for the read and write to 5 seconds.
                //sslStream.ReadTimeout = 5000;
                sslStream.WriteTimeout = 5000;

                while (true)
                {
                    string messageData;
                    // Read a message from the client.   
                    Console.WriteLine("Waiting for client message...");
                    try
                    {
                        messageData = Receiver.ReceiveString(sslStream);
                        //messageData = ReceiveString(sslStream);
                        //Console.WriteLine((char)7);   //Makes bell sound
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        HandleLogout(sslStream);
                        return;
                    }

                    string[] parsedMessage = ParseMessage(messageData);
                    StringHandler(parsedMessage, sslStream);
                    Console.WriteLine();
                }

                Console.ReadLine();
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                sslStream.Close();
                client.Close();
                return;
            }
            finally
            {
                // The client stream will be closed with the sslStream
                // because we specified this behavior when creating
                // the sslStream.
                sslStream.Close();
                client.Close();
            }
        }

        static string[] ParseMessage(string message)
        {
            return message.Split(Constants.MiddleDelimiter);
        }

        static void StringHandler(string[] input, SslStream sslStream)
        {
            //Check if client is logged in
            //Client that is not logged in, should only be able to get to HandleLogin
            if (userStreams.FirstOrDefault(x => x.Value == sslStream).Key == null)
            {
                switch (input[0])
                {
                    case "L":
                        HandleLogin(input, sslStream);
                        break;
                    default:
                        Console.WriteLine("Client is not logged in, and string is not recognized");
                        break;
                }
            }
            else  //if client is logged in
            {
                switch (input[0])
                {

                    case "W":
                        HandleMessage(input, userStreams.FirstOrDefault(x => x.Value == sslStream).Key, sslStream);
                        break;
                    case "L":
                        Console.WriteLine("User is already logged in");
                        Sender.SendString(sslStream, "You are already logged in");
                        break;
                    case "Q":
                        HandleLogout(sslStream);
                        break;
                    default:
                        Console.WriteLine("String is not recognized");
                        break;
                }

            }
        }

        static void HandleLogin(string[] input, SslStream sslStream)
        {
            //check if user is logged in
            //if (userStreams.FirstOrDefault(x => x.Value == sslStream).Key == null)
            {
                //Check if username is used
                if (userStreams.ContainsKey(input[1]))
                {
                    //username is in use
                    Console.WriteLine("Username: " + input[1] + " is in use");
                }
                else
                {
                    //Add to Dictionary
                    userStreams.Add(input[1], sslStream);
                }
            }
        }

        static void HandleLogout(SslStream sslStream)
        {
            Console.WriteLine("Handle logout");
            //remove from dictionary
            try
            {
                var keyFromValue = userStreams.FirstOrDefault(x => x.Value == sslStream).Key;
                if (keyFromValue != null)
                {
                    userStreams.Remove(userStreams.FirstOrDefault(x => x.Value == sslStream).Key);  //If user isn't logged in, dictionary remove will crash              
                }
                else
                {
                    Console.WriteLine("Client wasn't logged in");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //close connection
            CloseClientThread();
        }

        static void HandleMessage(string[] input, string login, SslStream sslStream)
        {
            //userStreams[input[1]].Write(Encoding.UTF8.GetBytes("From " + login + ": " + input[2] + "<EOF>"));
            if (userStreams.ContainsKey(input[1]))
            {
                Sender.SendString(userStreams[input[1]], "From " + login + ": " + input[2]);
            }
            else
            {
                Console.WriteLine("User " + input[1] + " isn't logged in");
                Sender.SendString(userStreams[login], "User: " + input[1] + " isn't logged in");
            }
        }

        static void FailedLogin()
        {
            //send message to client
        }

        static void CloseClientThread()
        {


        }

        static void DisplaySecurityLevel(SslStream stream)
        {
            Console.WriteLine("Cipher: {0} strength {1}", stream.CipherAlgorithm, stream.CipherStrength);
            Console.WriteLine("Hash: {0} strength {1}", stream.HashAlgorithm, stream.HashStrength);
            Console.WriteLine("Key exchange: {0} strength {1}", stream.KeyExchangeAlgorithm, stream.KeyExchangeStrength);
            Console.WriteLine("Protocol: {0}", stream.SslProtocol);
        }
        static void DisplaySecurityServices(SslStream stream)
        {
            Console.WriteLine("Is authenticated: {0} as server? {1}", stream.IsAuthenticated, stream.IsServer);
            Console.WriteLine("IsSigned: {0}", stream.IsSigned);
            Console.WriteLine("Is Encrypted: {0}", stream.IsEncrypted);
        }
        static void DisplayStreamProperties(SslStream stream)
        {
            Console.WriteLine("Can read: {0}, write {1}", stream.CanRead, stream.CanWrite);
            Console.WriteLine("Can timeout: {0}", stream.CanTimeout);
        }
        static void DisplayCertificateInformation(SslStream stream)
        {
            Console.WriteLine("Certificate revocation list checked: {0}", stream.CheckCertRevocationStatus);

            X509Certificate localCertificate = stream.LocalCertificate;
            if (stream.LocalCertificate != null)
            {
                Console.WriteLine("Local cert was issued to {0} and is valid from {1} until {2}.",
                    localCertificate.Subject,
                    localCertificate.GetEffectiveDateString(),
                    localCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Local certificate is null.");
            }
            // Display the properties of the client's certificate.
            X509Certificate remoteCertificate = stream.RemoteCertificate;
            if (stream.RemoteCertificate != null)
            {
                Console.WriteLine("Remote cert was issued to {0} and is valid from {1} until {2}.",
                    remoteCertificate.Subject,
                    remoteCertificate.GetEffectiveDateString(),
                    remoteCertificate.GetExpirationDateString());
            }
            else
            {
                Console.WriteLine("Remote certificate is null.");
            }
        }
        private static void DisplayUsage()
        {
            Console.WriteLine("To start the server specify:");
            Console.WriteLine("serverSync certificateFile.cer");
            Environment.Exit(1);
        }
        public static int Main(string[] args)
        {
            string certificate = null;
            if (args == null || args.Length < 1)
            {
                //DisplayUsage();
            }
            //certificate = args[0];
            //certificate = "D:/Users/Martin/Dropbox/IKT/4.Semester/PROJ4/Semesterprojekt-4/Feature test/SSL-Test/MartoTestCer.cer";

            Console.WriteLine("Write address to server certificate");
            certificate = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("************This is Server program************");

            listener.Start();

            //Keep listening and start new thread when client connects
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();  //Someone has connected

                Thread newThread =
                    new Thread(
                        unused => RunServer(certificate, client)    //The method where the thread is run
                    );

                newThread.Start();
            }
            return 0;
        }
    }
}