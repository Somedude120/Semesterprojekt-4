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
    class Message
    {
        public static void HandleMessage(string[] input, string login, SslStream sslStream, Dictionary<string, SslStream> userStreams)
        {
            //Check if users are friends
            if (GetAllMsgs.AreUsersFriends(login, input[1]))
            {
                //Check if user is online
                //Send to user
                if (userStreams.ContainsKey(input[1])) //If user is online
                {
                    Console.WriteLine("From: " + login + " to " + input[1]);
                    Sender.SendString(userStreams[input[1]],
                        "R" + Constants.GroupDelimiter + login + Constants.GroupDelimiter + input[1] + Constants.GroupDelimiter + input[2]);
                }
                else //User is not online
                {
                    Console.WriteLine("User " + input[1] + " isn't logged in");
                }

                //Save to database
                SaveMessages.SaveIncomingMessage(login, input[1], input[2]);
            }
        }
    }
}
