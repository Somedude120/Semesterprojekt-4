using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace TLSNetworking
{
    public class Sender
    {
        public static void SendString(SslStream sslStream, string message)
        {
            sslStream.Write(Encoding.UTF8.GetBytes(message + Constants.EndDelimiter));
        }
    }
}
