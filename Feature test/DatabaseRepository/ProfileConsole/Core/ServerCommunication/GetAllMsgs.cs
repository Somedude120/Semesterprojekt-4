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
            var person = unitOfWork.Chat.GetId(groupId);

            if (person.GroupId == groupId)
            {
                using (var db = new ProfileContext())
                {
                    var profile =
                        from p in db.Chat
                        where p.GroupId == groupId
                        select p;

                    try
                    {
                        foreach (var pers in profile)
                        {
                            if (groupId == pers.GroupId)
                            {
                                var sentMessages = new Chat(pers.GroupId, pers.MessageNumber, pers.Message, pers.Sender);
                                return sentMessages;
                            }
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
