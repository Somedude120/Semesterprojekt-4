using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class ChatRepo
    {
        public void CreateChatMessage(string users_messageNumber, string message, string from_User)
        {
            using (var db = new BloggingContext())
            {
                var chat = new Chat { Users_MessageNumber = users_messageNumber, Message = message, From_User = from_User };
                db.Chat.Add(chat);
                db.SaveChanges();

                Console.WriteLine("Users and messagenumber: " + users_messageNumber + " added message to their chat: " + message + "\n");
            }
        }

        public void ReadChatMessage()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.Chat
                            orderby b.Users_MessageNumber
                            select b;

                Console.WriteLine("All messages in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine("Chatmembers and messagenumber: " + item.Users_MessageNumber + ": " + item.Message + " --- message written by" + item.From_User);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
