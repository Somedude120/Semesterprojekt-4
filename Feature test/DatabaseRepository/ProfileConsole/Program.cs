using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                Repository myRepository = new Repository();
                //myRepository.CreateUserInformation("Daniel", "Darto");
                //myRepository.ReadUserInformation();
                //myRepository.deleteBrugerInformation("Sarto");
                //myRepository.CreateTag("SupportMain");
                myRepository.ReadTags();
            }
        }
    }
}
