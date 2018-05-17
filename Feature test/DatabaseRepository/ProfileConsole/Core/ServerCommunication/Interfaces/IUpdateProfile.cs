using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    public interface IUpdateProfile
    {
        void UpdateProfileInformation(string Username, string description, ICollection<Tags> tagslist);
    }
}
