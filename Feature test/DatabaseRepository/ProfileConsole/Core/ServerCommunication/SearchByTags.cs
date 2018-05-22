using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    public class SearchByTags : ISearchByTags
    {
        private IUnitOfWork unitOfWork;

        public SearchByTags()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public List<string> RequestTag(string tag)
        {

            var tags = unitOfWork.Tags.GetUserNamesWithTag(tag);
            var usernameList = new List<string>();

            foreach (var name in tags.UserInformation)
            {
                usernameList.Add(name.UserName);
            }
            
            return usernameList;

        }
    }
}
