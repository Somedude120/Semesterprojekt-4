using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    public class OtherProfile : IOtherProfile
    {
        public OtherProfile(string Username, string Description, ICollection<Tags> Tags)
        {
            username = Username;
            description = Description;
            tags = Tags;
            
        }
        public string username { get; set; }
        public string description { get; set; }
        public ICollection<Tags> tags { get; set; } 
    }
    
}
