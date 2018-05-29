﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace TLSNetworking
{
    public class Sender
    {
        public void SendString(SslStream sslStream, string message)
        {
            sslStream.Write(Encoding.ASCII.GetBytes(message + Constants.EndDelimiter));
        }
    }
}