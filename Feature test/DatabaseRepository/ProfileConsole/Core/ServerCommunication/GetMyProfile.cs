using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    // Gathers information from database to display the user's own profile
    public class GetMyProfile : IGetMyProfile
    {
        private IUnitOfWork unitOfWork;

        public GetMyProfile()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public MyProfile RequestOwnInformation(string Username, string Name, string Description, string Status,
            ICollection<FriendList> FriendList, ICollection<Tags> Tags)
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
                            MyProfile myProfile = new MyProfile(pers.UserName, pers.Name, pers.Description, pers.Status, pers.FriendList, pers.Tags);
                            return myProfile;
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
