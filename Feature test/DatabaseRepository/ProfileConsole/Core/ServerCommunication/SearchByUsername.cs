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

        public OtherProfile RequestUsername(String Username)
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
                            OtherProfile newProfile = new OtherProfile(pers.UserName, pers.Description, pers.Tags);
                            return newProfile;
                        }
                    }

                    catch (Exception)
                    {
                        return null;
                    }
                    
                }
                    
            }

            return null;

        }
    }
}
