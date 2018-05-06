using System;


using System.IO;


using System.Net;


using System.Threading;


using System.Net.Sockets;


using System.Security.Cryptography;


using System.Security.Cryptography.X509Certificates;


using System.Net.Security;








class Program
{
    static void Main(string[] args)
    {
        SecureTcpServer server = null;
        SecureTcpClient client = null;

        try


        {


            int port = 8889;





            RemoteCertificateValidationCallback certValidationCallback = null;


            certValidationCallback = new RemoteCertificateValidationCallback(IgnoreCertificateErrorsCallback);





            string certPath = System.Reflection.Assembly.GetEntryAssembly().Location;


            certPath = Path.GetDirectoryName(certPath);


            certPath = Path.Combine(certPath, "serverCert.cer");


            Console.WriteLine("Loading Server Cert From: " + certPath);


            X509Certificate serverCert = X509Certificate.CreateFromCertFile(certPath);





            server = new SecureTcpServer(port, serverCert,


                new SecureConnectionResultsCallback(OnServerConnectionAvailable));





            server.StartListening();





            client = new SecureTcpClient(new SecureConnectionResultsCallback(OnClientConnectionAvailable),


                certValidationCallback);





            client.StartConnecting("localhost", new IPEndPoint(IPAddress.Loopback, port));


        }


        catch (Exception ex)


        {


            Console.WriteLine(ex);


        }





        //sleep to avoid printing this text until after the callbacks have been invoked.


        Thread.Sleep(4000);


        Console.WriteLine("Press any key to continue...");


        Console.ReadKey();





        if (server != null)


            server.Dispose();


        if (client != null)


            client.Dispose();





    }





    static void OnServerConnectionAvailable(object sender, SecureConnectionResults args)


    {


        if (args.AsyncException != null)


        {


            Console.WriteLine(args.AsyncException);


            return;


        }





        SslStream stream = args.SecureStream;





        Console.WriteLine("Server Connection secured: " + stream.IsAuthenticated);











        StreamWriter writer = new StreamWriter(stream);


        writer.AutoFlush = true;





        writer.WriteLine("Hello from server!");





        StreamReader reader = new StreamReader(stream);


        string line = reader.ReadLine();


        Console.WriteLine("Server Recieved: '{0}'", line == null ? "<NULL>" : line);





        writer.Close();


        reader.Close();


        stream.Close();


    }





    static void OnClientConnectionAvailable(object sender, SecureConnectionResults args)


    {


        if (args.AsyncException != null)


        {


            Console.WriteLine(args.AsyncException);


            return;


        }


        SslStream stream = args.SecureStream;





        Console.WriteLine("Client Connection secured: " + stream.IsAuthenticated);





        StreamWriter writer = new StreamWriter(stream);


        writer.AutoFlush = true;





        writer.WriteLine("Hello from client!");





        StreamReader reader = new StreamReader(stream);


        string line = reader.ReadLine();


        Console.WriteLine("Client Recieved: '{0}'", line == null ? "<NULL>" : line);

        writer.Close();
        reader.Close();
        stream.Close();
    }

    static bool IgnoreCertificateErrorsCallback(object sender,
        X509Certificate certificate,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors)
    {
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            Console.WriteLine("IgnoreCertificateErrorsCallback: {0}", sslPolicyErrors);
            //you should implement different logic here...

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                foreach (X509ChainStatus chainStatus in chain.ChainStatus)
                {
                    Console.WriteLine("\t" + chainStatus.Status);
                }
            }
        }

        //returning true tells the SslStream object you don't care about any errors.
        return true;
    }
}