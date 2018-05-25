using ProfileConsole.Core.Domain;
using System.Collections.Generic;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    public interface IOtherProfile
    {
        string username { get; set; }
        string description { get; set; }
        ICollection<Tags> tags { get; set; }
    }
}
