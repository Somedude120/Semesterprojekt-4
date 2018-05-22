using System;
using System.Collections.Generic;
using System.Linq;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication
{
    public class UpdateProfile //: IUpdateProfile
    {
        //private IUnitOfWork unitOfWork;
        private static UnitOfWork unitOfWork;

        public UpdateProfile() 
        {
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        public static void UpdateProfileInformation(string Username, string Description, List<string> Tagslist)
        {
            unitOfWork = new UnitOfWork(new ProfileContext());

            UserInformation person = null;
            var tagsinDB = unitOfWork.Tags.GetAll();
            var tags = new List<Tags>();
            foreach (var tag in tagsinDB)
            {
                for (int i = 0; i <= Tagslist.Count; i++)
                {
                    if (tag.TagName == Tagslist[i])
                    {
                        Tagslist.RemoveAt(i);
                        Tagslist.TrimExcess();
                    }

                    else
                        tags.Add(new Tags{TagName = Tagslist[i]});
                }
                
            }
            
            //foreach (var tag in Tagslist)
            //{
                
            //    tags.Add(new Tags(){TagName = tag});
            //}

            try
            {
                person = unitOfWork.UserInformation.GetString(Username);
            }
            catch (Exception e)
            {

            }

            if (person.UserName == Username)
            {
                person.Description = Description;
                person.Tags = tags;
                unitOfWork.Complete();
            }
                        
                 

                    

                

                
            
        }
    }
}
