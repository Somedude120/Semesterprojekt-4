using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.Repositories;
using ProfileConsole.Persistence.Repositories;

namespace ProfileConsole.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ProfileContext _context;

        public UnitOfWork(ProfileContext context)
        {
            _context = context;
            ChatGroup = new ChatGroupRepo(_context);
            Chat = new ChatRepo(_context);
            Emoji = new EmojiRepo(_context);
            FriendList = new FriendListRepo(_context);
            LoginRepo = new LoginRepo(_context);
            Tags = new TagsRepo(_context);
            UserInformation = new UserInformationRepo(_context);
            
        }

        public IChatGroupRepo ChatGroup { get; }
        public IChatRepo Chat { get; }
        public IEmojiRepo Emoji { get; }
        public IFriendListRepo FriendList { get; }
        public ILoginRepo LoginRepo { get; }
        public ITagsRepo Tags { get; }
        public IUserInformationRepo UserInformation { get; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
