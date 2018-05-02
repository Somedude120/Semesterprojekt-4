using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class ChatRepo
    {
        private int messageNumber = 1;
        public void CreateChatMessage(int groupId, string message, string sender)
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.Chat select b;
                foreach (var item in query)
                {
                    if (item.MessageNumber == messageNumber && item.GroupId == groupId)
                    {
                        messageNumber++;
                    }
                }
                var chat = new Chat { GroupId = groupId, MessageNumber = messageNumber, Message = message, Sender = sender};
                db.Chat.Add(chat);
                db.SaveChanges();

                Console.WriteLine(sender + " to groupId: " + groupId + ": " + message + "\n");
            }
        }

        public void ReadChatMessage()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.Chat
                            orderby b.GroupId
                            select b;

                Console.WriteLine("All messages in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine("GroupId: " + item.GroupId + ": " + item.Message + " --- message written by " + item.Sender);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
