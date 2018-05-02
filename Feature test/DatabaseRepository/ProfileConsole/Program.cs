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
                myRepository.CreateUserInformation("Martin", "Marto");
                myRepository.ReadUserInformation();
                //myRepository.deleteBrugerInformation("Sarto");
                //myRepository.CreateTag("SupportMain");
            }
        }
    }
}
