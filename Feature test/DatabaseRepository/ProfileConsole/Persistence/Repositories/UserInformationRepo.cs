using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Repositories;
using ProfileConsole.Persistence.Repository;
using System.Data.Entity;

namespace ProfileConsole.Persistence.Repositories
{
    class UserInformationRepo : Repository<UserInformation>, IUserInformationRepo
    {
        public UserInformationRepo(ProfileContext context)
            : base(context)
        {
        }

        public UserInformation GetTagsWithUserInformation(string userName)
        {
            //return ProfileContext.UserInformation.OrderByDescending(c => c.UserName).Take(userName).ToList();
           return ProfileContext.UserInformation.Include(a => a.Tags).SingleOrDefault(a => a.UserName == userName);
        }

        public UserInformation GetChatGroupsWithUserInformation(string userName)
        {
            //return ProfileContext.UserInformation.OrderByDescending(c => c.UserName).Take(userName).ToList();
            return ProfileContext.UserInformation.Include(a => a.ChatGroups).SingleOrDefault(a => a.UserName == userName);
        }

        public ProfileContext ProfileContext
        {
            get { return Context as ProfileContext; }
        }
    }
}
