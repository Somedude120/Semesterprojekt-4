using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole
{
    class Repository
    {
        public void CreateUserInformation(string name, string userName)
        {
            using (var db = new BloggingContext())
            {
                var user = new UserInformation { Name = name, UserName = userName};
                db.UserInformation.Add(user);
                db.SaveChanges();

                Console.WriteLine("Tilføjede brugernavn: " + user.UserName + " til databasen\n");
            }
        }

        public void ReadUserInformation()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.UserInformation
                    orderby b.UserName
                    select b;

                Console.WriteLine("All users in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.UserName);
                }
                Console.WriteLine("\n");
            }
        }

        public void deleteUserInformation(string userName)
        {
            using (var db = new BloggingContext())
            {
                var DeleteUser =
                    from p in db.UserInformation
                    where p.UserName == userName
                    select p;

                foreach (var user in DeleteUser)
                {
                    db.UserInformation.Remove(user);
                    Console.WriteLine("Deleted user : " + user.UserName + " from the database\n");
                }

                try
                {
                    db.SaveChanges();
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("\n");
            }
        }

        public void CreateTag(string tagName)
        {
            using (var db = new BloggingContext())
            {
                var tag = new Tags { TagName = tagName };
                db.Tags.Add(tag);
                db.SaveChanges();

                Console.WriteLine("Created tag: " + tag.TagName + " to the database\n");
            }
        }

        public void ReadTags()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.Tags
                    orderby b.TagName
                    select b;

                Console.WriteLine("All tags in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.TagName);
                }
                Console.WriteLine("\n");
            }
        }

        public void DeleteTag(string tagName)
        {
            using (var db = new BloggingContext())
            {
                var deleteTag =
                    from p in db.Tags
                    where p.TagName == tagName
                    select p;

                foreach (var tag in deleteTag)
                {
                    db.Tags.Remove(tag);
                    Console.WriteLine("Deleted tag: " + tag.TagName + " from the database\n");
                }

                try
                {
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("\n");
            }
        }

        public void CreateFriendInformation (string user1, string user2, string status, string action_user)
        {
            using (var db = new BloggingContext())
            {
                var friendList = new FriendList { User1 = user1, User2 = user2, Status = status, Action_User = action_user};
                db.FriendList.Add(friendList);
                db.SaveChanges();

                Console.WriteLine("Created friendstatus between: " + friendList.User1 + " and " + friendList.User2 + " to " + friendList.Status + "\n");
            }
        }

        public void ReadFriendList()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.FriendList
                    orderby b.Status
                    select b;

                Console.WriteLine("All friendstatuses in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Status + ": " + item.User1 + " and " + item.User2+ ", last action made by: " + item.Action_User);
                }
                Console.WriteLine("\n");
            }
        }

        public void DeleteFriendInformation(string user1, string user2)
        {
            using (var db = new BloggingContext())
            {
                var deleteFriendInformation =
                    from p in db.FriendList
                    where p.User1 == user1 && p.User2 == user2
                    select p;

                foreach (var friendInformation in deleteFriendInformation)
                {
                    db.FriendList.Remove(friendInformation);
                    Console.WriteLine("Deleted friendInformation between " + friendInformation.User1 + " and " + friendInformation.User2 + " from the database\n");
                }

                try
                {
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("\n");
            }
        }

        public void UpdateFriendStatus(string user1, string user2, string newStatus, string action_user)
        {
            using (var db = new BloggingContext())
            {
                var deleteFriendInformation =
                    from p in db.FriendList
                    where p.User1 == user1 && p.User2 == user2
                    select p;

                foreach (var friendInformation in deleteFriendInformation)
                {
                    db.FriendList.Remove(friendInformation);
                }

                try
                {
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("\n");

                var friendList = new FriendList { User1 = user1, User2 = user2, Status = newStatus, Action_User = action_user };
                db.FriendList.Add(friendList);
                db.SaveChanges();
                Console.WriteLine("Updated status betweet " + user1 + " and " + user2 + " to " + newStatus);
            }
            Console.WriteLine("\n");
        }

        public void CreateChatMessage(string users_messageNumber, string message, string from_User)
        {
            using (var db = new BloggingContext())
            {
                var chat = new Chat { Users_MessageNumber = users_messageNumber, Message = message, From_User = from_User};
                db.Chat.Add(chat);
                db.SaveChanges();

                Console.WriteLine("Users and messagenumber: " + users_messageNumber + " added message to their chat: " + message + "\n");
            }
        }

        public void ReadChatMessage()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.Chat
                            orderby b.Users_MessageNumber
                            select b;

                Console.WriteLine("All messages in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine("Chatmembers and messagenumber: " + item.Users_MessageNumber + ": " + item.Message + " --- message written by" + item.From_User);
                }
                Console.WriteLine("\n");
            }
        }

        public void CreateEmoji(string emoji, string emojiShortcut)
        {
            using (var db = new BloggingContext())
            {
                var newEmoji = new Emoji { Emoji1 = emoji, EmojiShortcut = emojiShortcut};
                db.Emoji.Add(newEmoji);
                db.SaveChanges();

                Console.WriteLine("Created emoji: " + newEmoji.Emoji1 + " to the database\n");
            }
        }

        public void ReadEmojis()
        {
            using (var db = new BloggingContext())
            {
                var query = from b in db.Emoji
                    orderby b.Emoji1
                    select b;

                Console.WriteLine("All emojis in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Emoji1);
                }
                Console.WriteLine("\n");
            }
        }

        public void DeleteEmoji(string emojiShortcut)
        {
            using (var db = new BloggingContext())
            {
                var deleteEmoji =
                    from p in db.Emoji
                    where p.EmojiShortcut == emojiShortcut
                    select p;

                foreach (var emoji in deleteEmoji)
                {
                    db.Emoji.Remove(emoji);
                    Console.WriteLine("Deleted emoji: " + emoji.Emoji1 + " from the database\n");
                }

                try
                {
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("\n");
            }
        }

        public void UpdateEmojiShortcut(string emoji, string newEmojiShortcut)
        {
            using (var db = new BloggingContext())
            {
                var deleteEmoji =
                    from p in db.Emoji
                    where p.Emoji1 == emoji
                    select p;

                foreach (var item in deleteEmoji)
                {
                    db.Emoji.Remove(item);
                }

                try
                {
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("\n");

                var newEmoji = new Emoji { Emoji1 = emoji, EmojiShortcut = newEmojiShortcut };
                db.Emoji.Add(newEmoji);
                db.SaveChanges();
                Console.WriteLine("Updated shortcut for emoji: " + emoji + " to " +  newEmojiShortcut);
            }
        }
    }
}
