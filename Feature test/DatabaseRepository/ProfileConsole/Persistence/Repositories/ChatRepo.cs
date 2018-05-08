using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.Repositories;
using ProfileConsole.Persistence.Repository;

namespace ProfileConsole.Persistence.Repositories
{
    class ChatRepo : Repository<Chat>, IChatRepo
    {
        public ChatRepo(ProfileContext context)
            : base(context)
        {
        }

        //private int messageNumber = 1;
        //public void CreateChatMessage(int groupId, string message, string sender)
        //{
        //    using (var db = new BloggingContext())
        //    {
        //        var query = from b in db.Chat select b;
        //        foreach (var item in query)
        //        {
        //            if (item.MessageNumber == messageNumber && item.GroupId == groupId)
        //            {
        //                messageNumber++;
        //            }
        //        }

        public ProfileContext ProfileContext
        {
            get { return Context as ProfileContext; }
        }

        
    }
}
