using System;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class SearchByUsername
    {
        private IUnitOfWork unitOfWork;

        public SearchByUsername()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public OtherProfile RequestUsername(String name)
        {
            var person = unitOfWork.UserInformation.GetString(name);
            if (person.Name == name)
            {
                using (var db = new ProfileContext())
                {
                    var profile =
                    from p in db.UserInformation
                    where p.UserName == name 
                    select p;

                    try
                    {
                        var otherprofile = new OtherProfile();
                        

                    }

                    catch (Exception e)
                    {
                        return null;
                        
                    }
                }
                    
            }

            else
            {

            }
           
        }
    }
}
