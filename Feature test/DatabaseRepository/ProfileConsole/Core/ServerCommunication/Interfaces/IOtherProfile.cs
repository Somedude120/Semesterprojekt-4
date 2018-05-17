using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    public interface IOtherProfile
    {
        string username { get; set; }
        string description { get; set; }
        ICollection<Tags> tags { get; set; }
    }
}
