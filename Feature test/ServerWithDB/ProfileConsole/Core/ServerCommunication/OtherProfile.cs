using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using System.Collections.Generic;

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
