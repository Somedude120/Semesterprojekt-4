using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Core.Repositories
{
    public interface IUserInformationRepo : IRepository<UserInformation>
    {
        UserInformation GetTagsWithUserInformation(string userName);
        UserInformation GetChatGroupsWithUserInformation(string userName);
    }
}
