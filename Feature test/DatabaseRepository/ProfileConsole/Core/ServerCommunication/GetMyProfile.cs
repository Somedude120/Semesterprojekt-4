using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Linq;

namespace ProfileConsole.Core.ServerCommunication
{
    // Gathers information from database to display the user's own profile
    public class GetMyProfile
    {
        private IUnitOfWork unitOfWork;

        public GetMyProfile()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public OtherProfile RequestOwnInformation(String Username)
        {
            var person = unitOfWork.UserInformation.GetString(Username);

            if (person.UserName == Username)
            {
                using (var db = new ProfileContext())
                {
                    var profile =
                        from p in db.UserInformation
                        where p.UserName == Username
                        select p;


                    try
                    {
                        foreach (var pers in profile)
                        {

                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }
            }

            return null;
        }
    }
}
