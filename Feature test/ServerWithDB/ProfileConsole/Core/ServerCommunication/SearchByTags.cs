using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Linq;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    public class SearchByTags //: ISearchByTags
    {
        private static UnitOfWork unitOfWork;

        public SearchByTags()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static Tags RequestTag(string tag)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());

            try
            {
                var person = unitOfWork.Tags.GetString(tag);
                if (person.TagName == tag)
                    using (var db = new ProfileContext())
                    {
                        var profile =
                            from p in db.Tags
                            where p.TagName == tag
                            select p;

                        try
                        {
                            foreach (var tags in profile)
                            {
                                return new Tags {TagName = tags.TagName, UserInformation = tags.UserInformation};
                            }
                        }
                        catch (Exception e)
                        {
                            return null;
                        }
                    }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
