using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.ServerCommunication
{
    public class MyProfile
    {
        public MyProfile(string Description, List<Tags> Tags)
        {
            
            description = Description;
            
            tags = Tags;
        }

        
       
        public string description { get; set; }
        
        public List<Tags> tags { get; set; }
    }
}
