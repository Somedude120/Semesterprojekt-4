using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Threading;
using TLSNetworking;

//Taken from: https://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2

namespace Examples.System.Net
{
    public class SslTcpClient
    {
        public static Receiver receiver = new Receiver();
        public static Sender sender = new Sender();

        private static Hashtable certificateErrors = new Hashtable();

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
            Console.WriteLine("Client connected.");
            // Create an SSL stream that will close the client's stream.

            SslStream sslStream = new SslStream(
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
            byte[] messsage;
            
            Thread receiveThread = new Thread(o => ReceiveMessages((SslStream)o));
            receiveThread.Start(sslStream);

            while (true)
            {
                messsage = Encoding.UTF8.GetBytes(Console.ReadLine() + Constants.EndDelimiter);
                sslStream.Write(messsage);
                sslStream.Flush();
            }

            Console.ReadLine();
        }

        static void ReceiveMessages(SslStream sslStream)
        {
            string message;
            while (true)
            {
                message = receiver.ReceiveString(sslStream);
                Console.WriteLine(message);
            }
        }

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