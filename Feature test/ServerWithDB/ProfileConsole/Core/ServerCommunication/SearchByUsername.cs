using System;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class SearchByUsername //: ISearchByUsername
    {
        //private IUnitOfWork unitOfWork;
        private static UnitOfWork unitOfWork;

        public SearchByUsername()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static string RequestUsername(string Username)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
            string foundUsername = null;
            try
            {
                foundUsername = unitOfWork.UserInformation.GetString(Username).UserName;

            }
            catch (Exception e)
            {

            }

            return foundUsername;
        }
    }
}
