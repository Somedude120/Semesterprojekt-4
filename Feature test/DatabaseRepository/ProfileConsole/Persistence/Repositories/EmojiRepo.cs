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

namespace ProfileConsole.Persistence.Repositories
{
    class EmojiRepo : Repository<Emoji>, IEmojiRepo
    {
        public EmojiRepo(ProfileContext context)
            : base(context)
        {
        }

        public ProfileContext ProfileContext
        {
            get { return Context as ProfileContext; }
        }

    }
}
