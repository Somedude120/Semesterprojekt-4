using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class EmojiRepo
    {
        public void CreateEmoji(string emoji, string emojiShortcut)
        {
            using (var db = new BloggingContext())
            {
                var newEmoji = new Emoji { Emoji1 = emoji, EmojiShortcut = emojiShortcut };
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
                Console.WriteLine("Updated shortcut for emoji: " + emoji + " to " + newEmojiShortcut);
            }
        }
    }
}
