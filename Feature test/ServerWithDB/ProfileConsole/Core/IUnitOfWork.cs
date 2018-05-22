using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Repositories;

namespace ProfileConsole.Core
{
    public interface IUnitOfWork
    {
        IChatGroupRepo ChatGroup { get; }
        IChatRepo Chat { get; }
        IEmojiRepo Emoji { get; }
        IFriendListRepo FriendList { get; }
        ILoginRepo LoginRepo { get; }
        ITagsRepo Tags { get; }
        IUserInformationRepo UserInformation { get; }
        int Complete();
        void Dispose();
    }
}
