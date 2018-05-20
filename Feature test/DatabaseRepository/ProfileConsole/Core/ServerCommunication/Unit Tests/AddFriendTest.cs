using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole;
using ProfileConsole.Persistence;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NSubstitute;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication;
using ProfileConsole.Core.ServerCommunication.Interfaces;
namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
{
    [TestFixture]
    public class AddFriendTest
    {

        private string Username = "Fred5954";
        IAddFriend _uut;
        private IUnitOfWork unitOfWork;
        [SetUp]
        public void Setup()
        {
            _uut = new AddFriend();
            unitOfWork = new UnitOfWork(new ProfileContext());

        }

        [Test]
        public void AddFriendRequest_User1_IsCorrect()
        {
            _uut.AddFriendRequest(Username, "Bobby69");
            

            using (var db = new ProfileContext())
            {
                var profile =
                    from p in db.UserInformation
                    where p.UserName == Username
                    select p;

                foreach (var pers in profile)
                {
                    foreach (var friend in pers.FriendList)
                    {
                        Assert.That(friend.User1, Is.EqualTo(Username));
                    }
                }
            }
        }

        [Test]
        public void AddFriendRequest_User2_IsCorrect()
        {
            _uut.AddFriendRequest(Username, "Bobby69");


            using (var db = new ProfileContext())
            {
                var profile =
                    from p in db.UserInformation
                    where p.UserName == Username
                    select p;

                foreach (var pers in profile)
                {
                   Assert.That(pers.FriendList.ElementAt(0).User2, Is.EqualTo("Bobby69"));
                }
            }
        }

        [Test]
        public void AddFriendRequest_Status_IsCorrect()
        {

        }
    }
}
