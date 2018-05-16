using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.Repositories;
using ProfileConsole.Persistence.Repository;
using System.Data.Entity;

namespace ProfileConsole.Persistence.Repositories
{
    class TagsRepo : Repository<Tags>, ITagsRepo
    {
        public TagsRepo(ProfileContext context)
            : base(context)
        {
        }

        public Tags GetUserNamesWithTag(string tagName)
        {
            return ProfileContext.Tags.Include(a => a.UserInformation).SingleOrDefault(a => a.TagName == tagName);
        }

        public ProfileContext ProfileContext
        {
            get { return Context as ProfileContext; }
        }
    }
}
