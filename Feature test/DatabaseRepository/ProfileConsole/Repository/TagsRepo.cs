using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileConsole.Repository
{
    class TagsRepo
    {
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
    }
}
