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

//Taken from: https://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2

namespace Examples.System.Net
{
    public sealed class SslTcpServer
    {
        public static Dictionary<string, string> userID = new Dictionary<string, string>();
        public static Dictionary<string, SslStream> userStreams = new Dictionary<string, SslStream>();
        public static TcpListener listener = new TcpListener(IPAddress.Any, 443);
        static X509Certificate serverCertificate = null;
        // The certificate parameter specifies the name of the file 
        // containing the machine certificate.
        public static void RunServer(string certificate, TcpClient client)
        {
            serverCertificate = X509Certificate.CreateFromCertFile(certificate);
            // Create a TCP/IP (IPv4) socket and listen for incoming connections.
            //TcpListener listener = new TcpListener(IPAddress.Any, 8090);
            //TcpListener listener = new TcpListener(IPAddress.Any, 443);
            //listener.Start();
            //while (true)
            {
                //Console.WriteLine("Waiting for a client to connect...");
                // Application blocks while waiting for an incoming connection.
                // Type CNTL-C to terminate the server.
                //TcpClient client = listener.AcceptTcpClient();
                //Console.WriteLine("Client IP:" + ((IPEndPoint)client.Client.RemoteEndPoint).Address);
                //Console.WriteLine("Client port:" + ((IPEndPoint)client.Client.RemoteEndPoint).Port);
                ProcessClient(client);
            }
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

                ////Request UserID
                //string login;
                //try
                //{
                //    login = ReadMessage(sslStream);
                //}
                //catch (Exception e)
                //{
                //    return;
                //}
                //userID.Add(IPId, login);
                //userStreams.Add(login, sslStream);

                while (true)
                {
                    string messageData;
                    // Read a message from the client.   
                    Console.WriteLine("Waiting for client message...");
                    try
                    {
                        messageData = ReadMessage(sslStream);
                        //Console.WriteLine((char)7);   //Makes bell sound
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        handleLogout(sslStream);
                        return;
                    }
                    //Console.WriteLine("Received {0}: {1}", userID[IPId], messageData);

                    //Console.WriteLine(IPId);
                    //Console.WriteLine("From IP: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address + ", " + ((IPEndPoint)client.Client.RemoteEndPoint).Port);

                    // Write a message to the client.
                    //byte[] message = Encoding.UTF8.GetBytes("Access granted<EOF>");
                    //Console.WriteLine("Sending hello message.");
                    //sslStream.Write(message);

                    string[] parsedMessage = ParseMessage(messageData);
                    stringHandler(parsedMessage, sslStream);
                    //userStreams[parsedMessage[1]].Write(Encoding.UTF8.GetBytes("From " + login + ": " + parsedMessage[2] + "<EOF>"));
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
        static string ReadMessage(SslStream sslStream)
        {
            // Read the  message sent by the client.
            // The client signals the end of the message using the
            // "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                try
                {
                    bytes = sslStream.Read(buffer, 0, buffer.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Read Error");
                    Console.WriteLine(e);
                    throw;
                }
                // Read the client's test message.

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF or an empty message.
                if (messageData.ToString().IndexOf(Constants.EndDelimiter) != -1)
                {
                    messageData.Remove(messageData.ToString().IndexOf(Constants.EndDelimiter), Constants.EndDelimiter.Length);
                    break;
                }
            } while (bytes != 0);

            return messageData.ToString();
        }

        static string[] ParseMessage(string message)
        {
            return message.Split(Constants.MiddleDelimiter);
        }

        static void stringHandler(string[] input, SslStream sslStream)
        {
            //Check if client is logged in
            //Client that is not logged in, should only be able to get to handleLogin
            if (userStreams.FirstOrDefault(x => x.Value == sslStream).Key == null)
            {
                switch (input[0])
                {
                    case "L":
                        handleLogin(input, sslStream);
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
                        handleMessage(input, userStreams.FirstOrDefault(x => x.Value == sslStream).Key, sslStream);
                        break;
                    case "L":
                        Console.WriteLine("User is already logged in");
                        sendString(sslStream, "You are already logged in");
                        break;
                    case "Q":
                        handleLogout(sslStream);
                        break;
                    default:
                        Console.WriteLine("String is not recognized");
                        break;
                }

            }
        }

        static void handleLogin(string[] input, SslStream sslStream)
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

        static void handleLogout(SslStream sslStream)
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
            closeClientThread();
        }

        static void handleMessage(string[] input, string login, SslStream sslStream)
        {
            //userStreams[input[1]].Write(Encoding.UTF8.GetBytes("From " + login + ": " + input[2] + "<EOF>"));
            if (userStreams.ContainsKey(input[1]))
            {
                sendString(userStreams[input[1]], "From " + login + ": " + input[2]);
            }
            else
            {
                Console.WriteLine("User " + input[1] + " isn't logged in");
                sendString(userStreams[login], "User: " + input[1] + " isn't logged in");
            }
        }

        static void failedLogin()
        {
            //send message to client
        }

        static void closeClientThread()
        {


        }

        static void sendString(SslStream sslStream, string message)
        {
            Console.WriteLine(message);
            string delimiter = ((char)29).ToString();
            sslStream.Write(Encoding.ASCII.GetBytes(message + delimiter));
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
            certificate = "D:/Users/Martin/Dropbox/IKT/4.Semester/PROJ4/Semesterprojekt-4/Feature test/SSL-Test/MartoTestCer.cer";

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