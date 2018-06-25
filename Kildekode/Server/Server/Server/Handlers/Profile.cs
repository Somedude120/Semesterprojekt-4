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
    class Profile
    {
        public static void HandleGetProfile(string[] input, SslStream sslStream, Dictionary<string, SslStream> userStreams)
        {
            string username;

            //if no profile is specified, find the stream's owner's username
            if (input.Length == 1 || string.IsNullOrEmpty(input[1]))
            {
                username = userStreams.FirstOrDefault(x => x.Value == sslStream).Key;
            }
            else
            {
                username = input[1];
            }
            var profile = GetMyProfile.RequestOwnInformation(username);
            string messageToSend = null;

            try
            {
                messageToSend = Constants.GetProfile + Constants.GroupDelimiter + username + Constants.GroupDelimiter + profile.description + Constants.GroupDelimiter;
                var stringBuilder = new StringBuilder();
                foreach (var tag in profile.tags)
                {
                    stringBuilder.Append(tag.TagName);
                    stringBuilder.Append(Constants.DataDelimiter);
                }

                Console.WriteLine(messageToSend);
                messageToSend = messageToSend + stringBuilder.ToString();
                Sender.SendString(sslStream, messageToSend);
            }
            catch (Exception e)
            {
                Sender.SendString(sslStream, "RPNOK");  //Request Profile Not OK
            }

        }

        public static void HandleUpdateProfile(string[] input, SslStream sslStream, Dictionary<string, SslStream> userStreams)
        {
            string[] tags = Parsing.ParseData(input[3]);

            string username = userStreams.FirstOrDefault(x => x.Value == sslStream).Key;

            var tagList = new List<string>();

            //Capitalize all tags
            foreach (var tag in tags)
            {
                tagList.Add(tag.ToUpper());
            }

            UpdateProfile.UpdateProfileInformation(username, input[2], tagList);
        }

        public static void HandleDeleteProfile(string[] input, SslStream sslStream, Dictionary<string, SslStream> userStreams)
        {
            Console.WriteLine("Deleting profile");

            string username = userStreams.FirstOrDefault(x => x.Value == sslStream).Key;
            RemoveProfile.RemoveProfileRequest(username);
            userStreams.Remove(username);
        }
    }
}
