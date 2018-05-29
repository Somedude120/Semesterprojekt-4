using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Logout
    {
        public static void HandleLogout(SslStream sslStream, Dictionary<string, SslStream> userStreams, Mutex mutex)
        {
            Console.WriteLine("Handle logout");
            //remove from dictionary
            try
            {
                var keyFromValue = userStreams.FirstOrDefault(x => x.Value == sslStream).Key;
                if (keyFromValue != null)
                {
                    ProfileConsole.Core.ServerCommunication.Logout.LogoutDB(keyFromValue);

                    mutex.WaitOne();
                    Console.WriteLine(userStreams.FirstOrDefault(x => x.Value == sslStream).Key + " logged out");
                    userStreams.Remove(userStreams.FirstOrDefault(x => x.Value == sslStream).Key);  //If user isn't logged in, dictionary remove will crash 
                    mutex.ReleaseMutex();
                }
                else
                {
                    Console.WriteLine("Client wasn't logged in");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
