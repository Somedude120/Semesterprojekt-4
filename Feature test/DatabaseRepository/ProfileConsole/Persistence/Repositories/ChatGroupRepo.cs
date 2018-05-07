using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole;
using ProfileConsole.Core.Repositories;
using ProfileConsole.Persistence.Repository;

namespace ProfileConsole.Persistence.Repositories
{
    class ChatGroupRepo : Repository<ChatGroups>, IChatGroupRepo
    {
        public ChatGroupRepo(ProfileContext context)
            : base(context)
        {
        }



        public ProfileContext ProfileContext
        {
            get { return Context as ProfileContext; }
        }

        
    }
}
