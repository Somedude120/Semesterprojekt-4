using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.Repositories;
using ProfileConsole.Persistence.Repository;
using System.Data.Entity;

namespace ProfileConsole.Persistence.Repositories
{
    class ChatGroupRepo : Repository<ChatGroups>, IChatGroupRepo
    {
        public ChatGroupRepo(ProfileContext context)
            : base(context)
        {
        }

        public ChatGroups GetChatWithChatGroups(string groupName)
        {
            return ProfileContext.ChatGroups.Include(a => a.Chat).SingleOrDefault(a => a.GroupName == groupName);
        }

        public ProfileContext ProfileContext
        {
            get { return Context as ProfileContext; }
        }

        
    }
}
