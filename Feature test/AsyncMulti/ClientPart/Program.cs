using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int port = 5000;
        TcpClient client = new TcpClient();
        client.Connect(ip, port);
        Console.WriteLine("client connected!!");
        Console.WriteLine("Press enter to quit");
        NetworkStream ns = client.GetStream();
        Thread thread = new Thread(o => ReceiveData((TcpClient)o));

        thread.Start(client);

        string s = "0";
        Send(ip,s, ns);
        //while (!string.IsNullOrEmpty((s = Console.ReadLine())))
        //{
        //    byte[] buffer = Encoding.ASCII.GetBytes(s);
        //    ns.Write(buffer, 0, buffer.Length);
        //}

        client.Client.Shutdown(SocketShutdown.Send);
        thread.Join();
        ns.Close();
        client.Close();
        Console.WriteLine("disconnect from server!!");
        Console.ReadKey();
    }

    static void Send(IPAddress clientID,string s, NetworkStream ns)
    {
        while (!string.IsNullOrEmpty((s = Console.ReadLine())))
        {
            byte[] buffer = Encoding.ASCII.GetBytes(s);
            ns.Write(buffer, 0, buffer.Length);
        }
    }

    static void ReceiveData(TcpClient client)
    {
        NetworkStream ns = client.GetStream();
        byte[] receivedBytes = new byte[1024];
        int byte_count;

        while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
        {
            Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byte_count));
        }
    }
}