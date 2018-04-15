using System;
using System.Net.Sockets;
using System.Text;

namespace AsyncServer.Class
{
    public class ReturnData
    {
        private ReturnCall _sendcallback;

        public ReturnData(ReturnCall sendcallback)
        {
            _sendcallback = sendcallback;
        }

        public void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(_sendcallback.SendCallback), handler);
        }
    }
}