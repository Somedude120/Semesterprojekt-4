using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    //Gets all messages saved in the database
    public class GetAllMsgs //: IGetAllMsgs
    {
        private static IUnitOfWork unitOfWork;

        public GetAllMsgs()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static List<Chat> RequestAllMsgs(string username)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());

            var messages = unitOfWork.Chat.GetAll();
            var chatList = new List<Chat>();
            foreach (var message in messages)
            {
                if (message.Receiver == username || message.Sender == username)
                    chatList.Add(message);
            }

            return chatList;


        }
    }
}
