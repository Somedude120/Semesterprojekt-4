using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class UpdateProfile : IUpdateProfile
    {
        private IUnitOfWork unitOfWork;

        public UpdateProfile() 
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public void UpdateProfileInformation(string Username, string Description, ICollection<Tags> Tagslist)
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
                            pers.Description = Description;
                            pers.Tags = Tagslist;
                        }

                        unitOfWork.Complete();
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }

                
            }
        }
    }
}
