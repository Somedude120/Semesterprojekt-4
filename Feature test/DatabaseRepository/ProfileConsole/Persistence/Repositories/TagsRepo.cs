using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Repositories;
using ProfileConsole.Persistence.Repository;

namespace ProfileConsole.Persistence.Repositories
{
    class TagsRepo : Repository<Tags>, ITagsRepo
    {
        public TagsRepo(ProfileContext context)
            : base(context)
        {
        }

        public ProfileContext ProfileContext
        {
            get { return Context as ProfileContext; }
        }
    }
}
