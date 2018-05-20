using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;
namespace ProfileConsole.Core.ServerCommunication
{
    public class Logout : ILogout
    {
        private IUnitOfWork unitOfWork;

        public Logout()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public void LogoutDB(string Username)
        {
            var person = unitOfWork.UserInformation.GetString(Username);
            if(person.UserName == Username)
            { 
                using (var db = new ProfileContext())
                {
                    var profile =
                        from p in db.UserInformation
                        where p.UserName == Username
                        select p;

                    foreach (var p in profile)
                    {
                        p.Status = "Offline";
                    }
                    
                    unitOfWork.Complete();
                }
            }
        }

    }
}
