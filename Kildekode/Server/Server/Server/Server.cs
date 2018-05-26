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
using Microsoft.Win32;
using TLSNetworking;
using ProfileConsole;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication;
using Salt_And_Hash;
using Server;

//Taken from: https://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2

namespace Examples.System.Net
{
    public sealed class SslTcpServer
    {
        private static Mutex _mutex = new Mutex();
        public LoginRequest loginRequest = new LoginRequest();

        //public static Receiver receiver = new Receiver();
        //public static Sender sender = new Sender();

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
                Console.WriteLine();

                //string IPId = ((IPEndPoint)client.Client.RemoteEndPoint).Address + "," + ((IPEndPoint)client.Client.RemoteEndPoint).Port;

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
                        //Console.WriteLine((char)7);   //Makes bell sound
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Server.Logout.HandleLogout(sslStream, userStreams, _mutex);
                        return;
                    }

                    string[] parsedMessage = Parsing.ParseMessage(messageData);
                    MessageHandler.StringHandler(parsedMessage, sslStream, userStreams, _mutex);
                    Console.WriteLine();
                }

                //Console.ReadLine();
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

        public static int Main(string[] args)
        {
            string certificate = null;

            Console.WriteLine("Write address to server certificate");
            certificate = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("************ Marto Server is running ************");

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