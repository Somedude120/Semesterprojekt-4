using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TLSNetworking;

namespace Server
{
    class MessageHandler
    {
        public static void StringHandler(string[] input, SslStream sslStream, Dictionary<string, SslStream> userStreams, Mutex mutex)
        {
            //Check if client is logged in.
            //Client that is not logged in, should only be able to get to HandleLogin
            if (userStreams.FirstOrDefault(x => x.Value == sslStream).Key == null)  //If client isn't logged in
            {
                switch (input[0])
                {
                    case Constants.Signup:
                        if (input.Length == 3)
                        {
                            Signup.HandleSignup(input, sslStream);
                        }
                        break;
                    case Constants.RequestLogin:
                        if (input.Length == 3)
                        {
                            Console.WriteLine(input.Length);
                            Login.HandleLogin(input, sslStream, userStreams, mutex);
                        }
                        break;
                    default:
                        Console.WriteLine("Client is not logged in, and string is not recognized");
                        break;
                }
            }
            else  //if client is logged in
            {
                switch (input[0])
                {
                    case Constants.Write:   //Write message
                        Message.HandleMessage(input, userStreams.FirstOrDefault(x => x.Value == sslStream).Key, sslStream, userStreams);
                        break;
                    case Constants.RequestProfile:   //Profile get
                        Profile.HandleGetProfile(input, sslStream, userStreams);
                        break;
                    case Constants.UpdateProfile:   //Update profile
                        Profile.HandleUpdateProfile(input, sslStream, userStreams);
                        break;
                    case Constants.RequestLogin:   //Login
                        Console.WriteLine("User is already logged in");
                        Sender.SendString(sslStream, "You are already logged in");
                        break;
                    case Constants.Logout:   //Logout
                        Logout.HandleLogout(sslStream, userStreams, mutex);
                        break;
                    case Constants.SendFriendRequest:
                        FriendRequest.HandleSendFriendRequest(input, userStreams.FirstOrDefault(x => x.Value == sslStream).Key, sslStream, userStreams);
                        break;
                    case Constants.AcceptFriendRequest:
                        FriendRequest.HandleFriendRequestAccept(input, userStreams.FirstOrDefault(x => x.Value == sslStream).Key, sslStream, userStreams);
                        break;
                    case Constants.GetOldMessages:
                        OldMessages.HandleGetOldMessages(userStreams.FirstOrDefault(x => x.Value == sslStream).Key, sslStream);
                        break;
                    case Constants.RequestFriendList:
                        Friendlist.HandleGetFriendlist(userStreams.FirstOrDefault(x => x.Value == sslStream).Key, sslStream);
                        break;
                    case Constants.GetUsernamesByTag:
                        Search.HandleGetUserNamesByTag(input, sslStream);
                        break;
                    case Constants.RemoveFriend:
                        FriendRequest.HandleRemoveFriendRequest(input, userStreams.FirstOrDefault(x => x.Value == sslStream).Key, sslStream, userStreams);
                        break;
                    case Constants.DeleteProfile:
                        Profile.HandleDeleteProfile(input, sslStream, userStreams, mutex);
                        break;
                    default:
                        Console.WriteLine("String is not recognized");
                        break;
                }

            }
        }
    }
}
