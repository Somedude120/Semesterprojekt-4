using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.ServerCommunication
{
    public class OtherProfile
    {
        public OtherProfile(string Username, string Description, ICollection<Tags> Tags)
        {
            username = Username;
            description = Description;
            tags = Tags;
        }
        public string username = "";
        public string description = "";
        public ICollection<Tags> tags = new Collection<Tags>();
    }
}
