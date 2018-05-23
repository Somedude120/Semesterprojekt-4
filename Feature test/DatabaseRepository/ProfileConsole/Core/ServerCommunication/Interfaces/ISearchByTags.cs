using System.Collections.Generic;
using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    public interface ISearchByTags
    {
        List<string> RequestTag(string tag);
    }
}
