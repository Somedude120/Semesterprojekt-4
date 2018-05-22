using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;
namespace ProfileConsole.Core.ServerCommunication
{
    public class Logout //: ILogout
    {
        //private IUnitOfWork unitOfWork;
        private static UnitOfWork unitOfWork;

        public Logout()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static void LogoutDB(string Username)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());

            UserInformation person = null;
            try
            {
                person = unitOfWork.UserInformation.GetString(Username);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if(person.UserName == Username)
            { 
                using (var db = new ProfileContext())
                {
                    person.Status = "Offline";
                    unitOfWork.Complete();
                }
            }
        }

    }
}
