using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class SignUp : ISignUp
    {
        private IUnitOfWork unitOfWork;

        public SignUp()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public string CreateProfile(string Username, string salt, string hash)
        {
            
            var person = unitOfWork.UserInformation.GetString(Username);

            if (person != null)
            {
                const string errormessage = "Username already exists";
                return errormessage;
            }


            var newUser = new UserInformation
            {
                UserName = Username,
                Login = new Login
                {
                    Hash = hash,
                    Salt = salt
                }

            };

            unitOfWork.UserInformation.Add(newUser);
            unitOfWork.Complete();

            var userCreated = "OK";
            return userCreated;

        }

        
    }
}
