using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Castle.Core.Internal;
using NSubstitute;
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

            try
            {
                person = unitOfWork.UserInformation.GetString(Username);
            }
            catch (Exception e)
            {

            }

            var isInList = false;
            //if (tagsinDB.IsNullOrEmpty())
            //{
            //    foreach (var tag in Tagslist )
            //    {
            //        person.Tags.Add(new Tags() { TagName = tag });
            //        //unitOfWork.Tags.Add(new Tags(){TagName = tag});
            //    }
                
            //}
            //else
            //{
                foreach (var newTag in Tagslist)
                {

                    foreach (var tag in tagsinDB)
                    {
                        if (tag.TagName == newTag)
                        {
                            isInList = true;
                            person.Tags.Add(tag);
                            break;
                        }


                        //else 
                        //{
                        //    person.Tags.Add(new Tags() { TagName = newTag });
                        //}
                    }

                    if (!isInList)
                    {
                        person.Tags.Add(new Tags() { TagName = newTag });
                    }

            }

                    //if (isInList)
                    //{
                    //    person.Tags.Add();
                    //    //person.Tags.Add(new Tags() { TagName = newTag });
                    //    isInList = false;

                    //}
                    //else
                    //{
                    //    person.Tags.Add(new Tags() { TagName = newTag });
                    //    //unitOfWork.Tags.Add(new Tags() {TagName = newTag});
                    //    //tags.Add(new Tags() {TagName = newTag});
                    //}
            //}

            //if (person.Tags.IsNullOrEmpty())
            //{
            //    foreach (var tag in tags)
            //    {
            //        person.Tags.Add(tag);                    
            //    }
            //}
            //else
            //{ 
            //    foreach (var newTag in tags)
            //    {

            //        foreach (var tag in person.Tags)
            //        {
            //            if (tag.TagName == newTag.TagName)
            //            {
            //                isInList = true;
            //                break;
            //            }
            //        }
            //        if (!isInList)
            //        {
            //            //person.Tags.Add(new Tags(){TagName = newTag.TagName});
            //        }
            //        isInList = false;

            //    }
            //}

            person.Description = Description;
            
            unitOfWork.Complete();










        }
    }
}
