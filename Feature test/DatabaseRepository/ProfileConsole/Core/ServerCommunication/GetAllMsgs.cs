using System;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    //Gets all messages saved in the database
    public class GetAllMsgs : IGetAllMsgs
    {
        private IUnitOfWork unitOfWork;

        public GetAllMsgs()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public Chat RequestAllMsgs(int groupId, int messageNumber, string message, string sender)
        {
            var chat = unitOfWork.Chat.GetId(groupId);

            if (chat.GroupId == groupId)
            {
                using (var db = new ProfileContext())
                {
                    var chats =
                        from c in db.Chat
                        where c.GroupId == groupId
                        select c;

                    try
                    {
                        foreach (var cha in chats)
                        {
                            Chat myChat = new Chat(cha.GroupId, cha.MessageNumber, cha.Message, cha.Sender);
                            return myChat;
                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }
            }

            return null;
        }
    }
}
