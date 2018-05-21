using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.ServerCommunication.Interfaces
{
    interface IGetAllMsgs
    {
        Chat RequestAllMsgs(int groupId, int messageNumber, string message, string sender);
        int groupId { get; set; }
        int messageNumber { get; set; }
        string message { get; set; }
        string sender { get; set; }
    }
}
