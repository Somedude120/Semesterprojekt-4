using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.ServerCommunication;
using Salt_And_Hash;
using TLSNetworking;

namespace Server
{
    class Signup
    {
        public static void HandleSignup(string[] input, SslStream sslStream)
        {
            var saltHash = new SaltedHash();

            string salt = saltHash.MakeSalt();
            string hashedPW = saltHash.ComputeHash(salt, input[2]);

            string result = SignUp.CreateProfile(input[1], salt, hashedPW);

            if (result == "OK")
            {
                Console.WriteLine("User: " + input[1] + " created");
                Sender.SendString(sslStream, "SOK");
            }
            else
            {
                Console.WriteLine("User not created");
                Sender.SendString(sslStream, "SNOK");
            }
        }
    }
}
