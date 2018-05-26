using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProfileConsole.Core.ServerCommunication;
using Salt_And_Hash;
using TLSNetworking;

namespace Server
{
    class Login
    {
        public static void HandleLogin(string[] input, SslStream sslStream, Dictionary<string, SslStream> userStreams, Mutex mutex)
        {
            var saltHash = new SaltedHash();

            //Check if username is already in logged in
            if (userStreams.ContainsKey(input[1]))
            {
                //username is in use
                Console.WriteLine("Username: " + input[1] + " is already online");
                Sender.SendString(sslStream, "NOK");
            }
            else
            {
                //check if user is in database
                if (SearchByUsername.RequestUsername(input[1]) != null)
                {
                    Console.WriteLine("User: " + input[1] + " exists in database");

                    //Calculate hashed password with pw and salt
                    string salt = LoginRequest.GetSalt(input[1]);
                    string hashedPW = saltHash.ComputeHash(salt, input[2]);

                    //Make LoginRequest
                    string response = LoginRequest.Login(input[1], hashedPW);
                    if (response == "OK")
                    {
                        //Add to Dictionary
                        mutex.WaitOne();
                        userStreams.Add(input[1], sslStream);
                        mutex.ReleaseMutex();
                        Console.WriteLine("User logged in with username: " + input[1]);
                        Sender.SendString(sslStream, "OK");
                    }
                    else
                    {
                        Console.WriteLine("Username or password was wrong");
                        Sender.SendString(sslStream, "NOK");
                    }
                }
                else
                {
                    Console.WriteLine("User: " + input[1] + " does not exist in database");
                    Sender.SendString(sslStream, "NOK");
                }
            }
        }
    }
}
