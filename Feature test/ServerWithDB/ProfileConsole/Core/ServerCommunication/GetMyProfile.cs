using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication
{
    // Gathers information from database to display the user's own profile
    public class GetMyProfile// : IGetMyProfile
    {
        private static IUnitOfWork unitOfWork;

        public GetMyProfile()
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static MyProfile RequestOwnInformation(string Username)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
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
                        var tagList = new List<Tags>();
                        foreach (var pers in profile)
                        {
                            var temp = unitOfWork.UserInformation.GetTagsWithUserInformation(pers.UserName);
                            foreach (var tag in temp.Tags)
                            {
                                tagList.Add(tag);
                            }
                            MyProfile myProfile = new MyProfile(pers.Description, tagList);
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
