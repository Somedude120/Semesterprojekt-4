using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.ServerCommunication;
using TLSNetworking;

namespace Server
{
    class OldMessages
    {
        public static void HandleGetOldMessages(string login, SslStream sslStream)
        {
            var messages = GetAllMsgs.RequestAllMsgs(login);
            foreach (var message in messages)
            {
                string messageToSend = Constants.MessageReceived + Constants.GroupDelimiter + message.Sender + Constants.GroupDelimiter +
                                message.Receiver + Constants.GroupDelimiter + message.Message;
                Sender.SendString(sslStream, messageToSend);
            }
        }
    }
}
