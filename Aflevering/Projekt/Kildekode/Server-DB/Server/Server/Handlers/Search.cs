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
    class Search
    {
        public static void HandleGetUserNamesByTag(string[] input, SslStream sslStream)
        {
            var tag = SearchByTags.RequestTag(input[1].ToUpper());

            try
            {
                var tags = tag.UserInformation;
                var stringBuilder = new StringBuilder();

                foreach (var userInformation in tags)
                {
                    stringBuilder.Append(userInformation.UserName);
                    stringBuilder.Append(Constants.DataDelimiter);
                }

                Sender.SendString(sslStream, Constants.GetUsernamesByTag + Constants.GroupDelimiter + stringBuilder.ToString());
            }
            catch (Exception e)
            {
                Sender.SendString(sslStream, Constants.GetUsernamesByTag + Constants.GroupDelimiter);
            }
        }
    }
}
