using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class SearchByTags
    {
        private IUnitOfWork unitOfWork;

        public SearchByTags()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public OtherProfile RequestTag(string tag)
        {
            var person = unitOfWork.Tags.GetString(tag);
            if(person.TagName == tag)
            using (var db = new ProfileContext())
            {
                var profile =
                    from p in db.Tags
                    where p.TagName == tag 
                    select p;

                try
                {
                    foreach (var pers in profile)
                    {
                        //return new OtherProfile(pers.UserName, pers.Description, pers.Tags);
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            return null;
        }
    }
}
