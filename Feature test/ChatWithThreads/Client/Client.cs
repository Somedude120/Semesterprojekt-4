using System;
using System.Net.Sockets;
using System.Threading;

//Code is from https://www.codeproject.com/Articles/511814/Multi-client-per-one-server-socket-programming-in
public class Program
{
    static public void Main(string[] Args)
    {
        //var clients = new Client[1000];
        //for (int i = 0; i < 100; i++)
        //{
        //    clients[i] = new Client();
        //    clients[i].MakeClient(i);
        //}

        //for (int i = 0; i < 100; i++)
        //{
        //    clients[i].SendMessage($"{i}");
        //}

        var client = new Client();

        Console.ReadLine();
    }
}

public class Client
{
    private TcpClient socketForServer;
    private NetworkStream networkStream;
    private System.IO.StreamWriter streamWriter;

    public void MakeClient(int number)
    { 
        try
        {
            socketForServer = new TcpClient("localHost", 10);
        }
        catch
        {
            Console.WriteLine(
            "Failed to connect to server at {0}:999", "localhost");
            return;
        }
       
        networkStream = socketForServer.GetStream();
        System.IO.StreamReader streamReader =
        new System.IO.StreamReader(networkStream);
        streamWriter = new System.IO.StreamWriter(networkStream);
        Console.WriteLine("*******This is client program who is connected to localhost on port No:10*****");
        
        //try
        //{
        //    string outputString;
        //    // read the data from the host and display it
        //    {
        //        //outputString = streamReader.ReadLine();
        //        //Console.WriteLine("Message Recieved by server:" + outputString);

        //        //Console.WriteLine("Type your message to be recieved by server:");
        //        Console.WriteLine("type:");
        //        string str = Console.ReadLine();
        //        while (str != "exit")
        //        {
        //            streamWriter.WriteLine(str);
        //            streamWriter.Flush();
        //            Console.WriteLine("type:");
        //            str = Console.ReadLine();
        //        }
        //        if (str == "exit")
        //        {
        //            streamWriter.WriteLine(str);
        //            streamWriter.Flush();
                   
        //        }
                
        //    }
        //}
        //catch
        //{
        //    Console.WriteLine("Exception reading from Server");
        //}
        //// tidy up
        //networkStream.Close();
        //Console.WriteLine("Press any key to exit from client program");
        //Console.ReadKey();
    }

    public void SendMessage(string msg)
    {
        streamWriter.WriteLine(msg);
    }

    private static string GetData()
    {
        //Ack from sql server
        return "ack";
    }
}