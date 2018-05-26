using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Parsing
    {
        public static string[] ParseMessage(string message)
        {
            Console.WriteLine(message);
            return message.Split(Constants.GroupDelimiter);
        }

        public static string[] ParseData(string dataCollection)
        {
            return dataCollection.Split(Constants.DataDelimiter);
        }
    }
}
