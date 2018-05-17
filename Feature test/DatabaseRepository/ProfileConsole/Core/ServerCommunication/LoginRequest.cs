using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;
namespace ProfileConsole.Core.ServerCommunication
{
    public class LoginRequest : ILoginRequest
    {
        private IUnitOfWork unitOfWork;

        public LoginRequest()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public string Login(string Username, string Hash)
        {
            var person = unitOfWork.UserInformation.GetString(Username);

            if (person != null)
            { 
                if (person.UserName == Username && person.Login.Hash == Hash)
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
                                pers.Status = "Online";
                            }

                            unitOfWork.Complete();
                            const string OK = "OK";
                            return OK;
                        }

                        catch (Exception e)
                        {
                            return null;
                        }

                    }
            }
            }

            const string WrongInfo = "IncorrectLoginInfo";
            return WrongInfo;
        }
    }
}
