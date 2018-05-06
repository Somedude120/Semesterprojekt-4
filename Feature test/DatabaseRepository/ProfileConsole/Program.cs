using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Repository;

namespace ProfileConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                ChatRepo chat = new ChatRepo();
                ChatGroupRepo group = new ChatGroupRepo();
                EmojiRepo emoji = new EmojiRepo();
                FriendListRepo friendList = new FriendListRepo();
                LoginRepo login = new LoginRepo();
                TagsRepo tag = new TagsRepo();
                UserChatGroupRepo userChatGroup = new UserChatGroupRepo();
                UserInformationRepo user = new UserInformationRepo();
                UserTagsRepo userTag= new UserTagsRepo();

                //Using repository
                userTag.ReadUserTags();


            }
        }
    }
}
