using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    public interface IGetAllMsgs
    {
        Chat RequestAllMsgs(int groupId, int messageNumber, string message, string sender);
    }
}
