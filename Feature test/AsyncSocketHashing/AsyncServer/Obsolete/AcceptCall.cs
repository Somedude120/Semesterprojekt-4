using System;
using System.Net.Sockets;
using System.Threading;

namespace AsyncServer.Class
{
    public class AcceptCall
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private ReadCall _readcallback;

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(_readcallback.ReadCallback), state);
        }
    }
}