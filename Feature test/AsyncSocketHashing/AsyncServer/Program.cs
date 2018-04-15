using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using AsyncServer.Class;


public class Program
{
    
    public static int Main(String[] args)
    {
        AsynchronousSocketListener _server = new AsynchronousSocketListener();
        //AcceptCall accept = new AcceptCall();
        //ReadCall read = new ReadCall();
        //ReturnCall rcall = new ReturnCall();
        //ReturnData rData = new ReturnData(rcall);
        //StateObject stateObject = new StateObject();


        //rcall.SendCallback(arg);

        

        _server.StartListening();
        
        return 0;
    }
}