using System.Collections.Generic;
using MartUI.Me;

namespace ProfileConsole.Core.ServerCommunication
{
    public class OtherProfile
    {
        //Objectification of OtherProfile, ready for serialization
        public MyData _userData;
        public OtherProfile()
        {

            username = _userData.Username;
            description = _userData.Description;
            tags = _userData.Tags;
            
        }
        public string username { get; set; }
        public string description { get; set; }
        public ICollection<string> tags { get; set; } 
    }
    
}
