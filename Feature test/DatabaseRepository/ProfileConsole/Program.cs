using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole.Core.Domain;
using ProfileConsole.Persistence;
using ProfileConsole.Core.ServerCommunication;
using ProfileConsole.Persistence.Repositories;

namespace ProfileConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var SBU = new SearchByUsername();
            var profile = SBU.RequestUsername("Fred5954");
            Console.WriteLine(profile.username);
            using (var unitOfWork = new UnitOfWork(new ProfileContext()))
            {
                var userTags = unitOfWork.UserInformation.GetTagsWithUserInformation("Farto");
                var userGroups = unitOfWork.UserInformation.GetChatGroupsWithUserInformation("Farto");
                var chatGroups = unitOfWork.ChatGroup.GetChatWithChatGroups("Marto-entutiaster");

                
                //Console.WriteLine("\nGroupName: " + chatGroups.GroupName);
                //foreach (var item in chatGroups.Chat)
                //{
                //    Console.WriteLine("\t" + item.Sender + ": " + item.Message);    
                //}

                //Console.WriteLine("\nUserName: " + userTags.UserName);
                //foreach (var item in userTags.Tags)
                //{
                //    Console.WriteLine("\t Tag: " + item.TagName);
                //}

                //Console.WriteLine("\nUserName: " + userGroups.UserName);
                //foreach (var item in userGroups.ChatGroups)
                //{
                //    Console.WriteLine("\t ChatGroup: " + item.GroupName);
                //}
               

                //----------Add----------
                //var tag = new Tags { TagName = "1v1" };
                //unitOfWork.Tags.Add(tag);
                //unitOfWork.Complete();


                //----------PrintAll----------
                //var getTags = unitOfWork.Tags.GetAll();
                //foreach (var tag in getTags)
                //{
                //    Console.WriteLine(tag.TagName);
                //}


                //----------Remove----------
                //var tag = unitOfWork.Tags.GetString("1v1");
                //unitOfWork.Tags.Remove(tag);
                //unitOfWork.Complete();
            }
        }

        //static void PrintUserInfo(IEnumerable<>);
        //foreach (var u in userTags)
        //{
        //    Console.WriteLine("UserName: " + u.UserName);
        //    foreach (var t in u.Tags)
        //    {
        //        Console.WriteLine("\t Tag: " + t.TagName);
        //    }
        //}
    }
}
