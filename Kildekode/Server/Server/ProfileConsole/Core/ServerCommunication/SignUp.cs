using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class SignUp //: ISignUp
    {
        //private IUnitOfWork unitOfWork;
        private static UnitOfWork unitOfWork;

        public SignUp()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static string CreateProfile(string Username, string salt, string hash)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
            const string errormessage = "Username already exists";
            UserInformation person = null;
            try
            {
                person = unitOfWork.UserInformation.GetString(Username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (person != null)
            {
                return errormessage;
            }


            var newUser = new UserInformation
            {
                UserName = Username,
                Login = new Login
                {
                    Username = Username,
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
