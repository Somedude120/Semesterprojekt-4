using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;


//Code is taken from: http://csharp.net-informations.com/communications/csharp-server-socket.htm
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            var serverSocket = new TcpListener(8888);   //Listens on port 8888
            var clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine("Server started");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("Accept connection from client");

            //Loop
            while (true)
            {
                try
                {
                    var networkStream = clientSocket.GetStream();   //Open a stream
                    var bytesFrom = new byte[10025];
                    networkStream.Read(bytesFrom, 0, (int) clientSocket.ReceiveBufferSize); //Read from stream
                    var dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);   //Convert byteArray to string
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));  //"$" is the end character from client
                    Console.WriteLine("Data from client: " + dataFromClient);

                    var serverResponse = "Last message from client: " + dataFromClient; //Creates string
                    var sendBytes = Encoding.ASCII.GetBytes(serverResponse);    //Converts string to byteArray
                    networkStream.Write(sendBytes, 0, sendBytes.Length);    //Sends to client
                    networkStream.Flush();
                    Console.WriteLine(serverResponse);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Cleaning
            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("Exit");
            Console.ReadKey();
        }
    }
}
