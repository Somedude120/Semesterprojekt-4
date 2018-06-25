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
    class FriendRequest
    {
        public static void HandleSendFriendRequest(string[] input, string login, SslStream sslStream, Dictionary<string, SslStream> userStreams)
        {
            AddFriend.AddFriendRequest(login, input[1]);

            //Check if username is already in logged in
            if (userStreams.ContainsKey(input[1]))
            {
                Sender.SendString(userStreams[input[1]], "FRR" + Constants.GroupDelimiter + login);
            }
            else
            {
                Console.WriteLine("Send friendrequest. User: " + input[1] + " is not online");
            }
        }

        public static void HandleRemoveFriendRequest(string[] input, string login, SslStream sslStream, Dictionary<string, SslStream> userStreams)
        {
            RemoveFriend.RemoveFriendRequest(login, input[1]);
            Console.WriteLine("Removed friend. Username: " + login + " Removed friend: " + input[1]);
        }

        public static void HandleFriendRequestAccept(string[] input, string login, SslStream sslStream, Dictionary<string, SslStream> userStreams)
        {
            AcceptFriendRequest.AcceptRequest(login, input[1]);

            //Check if username is already in logged in
            if (userStreams.ContainsKey(input[1]))
            {
                Sender.SendString(userStreams[input[1]], "FRA" + Constants.GroupDelimiter + login);
            }
            else
            {
                Console.WriteLine("Accept friendrequest. User: " + input[1] + " is not online");
            }
        }
    }
}
