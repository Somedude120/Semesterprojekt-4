using NSubstitute;
using NUnit.Framework;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
{
    [TestFixture]
    public class GetMyProfileTest
    {
        private string Username = "AlexD";
        private string Description = "Jeg er seg";
        private string Status = "Online";
        private string TagName = "Kødbolle";
        private ICollection<FriendList> friendlist;
        private ICollection<Tags> tags;

        IGetMyProfile _uut;
        IMyProfile myProfile;

        [SetUp]
        public void Setup()
        {
            myProfile = Substitute.For<IMyProfile>();
            _uut = new GetMyProfile();
            myProfile.username = Username;
            myProfile.description = Description;
            myProfile.status = Status;
            friendlist = new Collection<FriendList>();
            myProfile.friendlist = friendlist;
            tags = new Collection<Tags>();
            myProfile.tags = tags;
        }

        [Test]
        public void Profile_Username_IsInDB_Returns_Correctly()
        {
            var returnProfile = _uut.RequestOwnInformation(Username, Description, Status, friendlist, tags);
            Assert.That(returnProfile.username, Is.EqualTo(myProfile.username));
        }


        [Test]
        public void Profile_Description_IsInDB_Returns_Correctly()
        {
            var returnProfile = _uut.RequestOwnInformation(Username, Description, Status, friendlist, tags);
            Assert.That(returnProfile.description, Is.EqualTo(myProfile.description));
        }

        [Test]
        public void Profile_Status_IsInDB_Returns_Correctly()
        {
            var returnProfile = _uut.RequestOwnInformation(Username, Description, Status, friendlist, tags);
            Assert.That(returnProfile.status, Is.EqualTo(myProfile.status));
        }

        [Test]
        public void Profile_Friendlist_IsInDB_Returns_Correctly()
        {
            var returnProfile = _uut.RequestOwnInformation(Username, Description, Status, friendlist, tags);
            Assert.That(returnProfile.friendlist, Is.EqualTo(myProfile.friendlist));
        }

        [Test]
        public void Profile_Tags_IsInDB_Returns_Correctly()
        {
            var returnProfile = _uut.RequestOwnInformation(Username, Description, Status, friendlist, tags);
            Assert.That(returnProfile.tags, Is.EqualTo(myProfile.tags));
        }
    }
}
