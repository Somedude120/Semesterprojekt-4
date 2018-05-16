using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MartUI.Chat
{
    public class ChatModel
    {
        private string _message;
        private string _sender;
        private string _receiver;
        private string _messagePosition;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        public string Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        public string MessagePosition
        {
            get { return _messagePosition; }
            set { _messagePosition = value; }
        }
    }
}
