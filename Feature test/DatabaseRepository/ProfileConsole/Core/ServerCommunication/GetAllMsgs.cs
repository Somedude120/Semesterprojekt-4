using System;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    //Gets all messages saved in the database
    public class GetAllMsgs
    {
        private IUnitOfWork unitOfWork;

        public GetAllMsgs()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public Chat RequestAllMsgs(int GroupID, int MessageNumber, string Message, string Sender)
        {
            var chat = unitOfWork.Chat.GetId(GroupID);

            if (chat.GroupId == GroupID)
            {
                using (var db = new ProfileContext())
                {
                    var chats =
                        from c in db.Chat
                        where c.GroupId == GroupID
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
