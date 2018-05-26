using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace TLSNetworking
{
    public class Receiver
    {
        public static string ReceiveString(SslStream sslStream)
        {
            // Read the  message sent by the client.
            // The client signals the end of the message using the
            // "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                try
                {
                    bytes = sslStream.Read(buffer, 0, buffer.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Read Error");
                    Console.WriteLine(e);
                    throw;
                }
                // Read the client's test message.

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF or an empty message.
                if (messageData.ToString().IndexOf(Constants.EndDelimiter) != -1)
                {
                    messageData.Remove(messageData.ToString().IndexOf(Constants.EndDelimiter), 1);
                    break;
                }
            } while (bytes != 0);

            return messageData.ToString();
        }

    }
}
