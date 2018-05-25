using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    public class SaveMessages
    {
        private static IUnitOfWork unitOfWork;

        public SaveMessages()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }


        public static void SaveIncomingMessage(string sender, string receiver, string message)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());

            using (var db = new ProfileContext())
            {
                var profile =
                    from p in db.Chat
                    where p.Sender == sender && p.Receiver == receiver
                    select p;



                var chat = new Chat
                {
                    Sender = sender,
                    Receiver = receiver,
                    Message = message
                   
                };


                unitOfWork.Chat.Add(chat);

                unitOfWork.Complete();
            }

        }
    }
}
