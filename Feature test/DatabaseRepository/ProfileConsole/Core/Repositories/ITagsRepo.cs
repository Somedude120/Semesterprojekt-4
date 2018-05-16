using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;

namespace ProfileConsole.Core.Repositories
{
    public interface ITagsRepo : IRepository<Tags>
    {
        Tags GetUserNamesWithTag(string tagName);
    }
}
