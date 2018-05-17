using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    interface ISearchByTags
    {
        Tags RequestTag(string tag);
    }
}
