using System;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class SearchByUsername : ISearchByUsername
    {
        private IUnitOfWork unitOfWork;

        public SearchByUsername()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public UserInformation RequestUsername(string Username)
        {
            return unitOfWork.UserInformation.GetString(Username);
        }
    }
}
