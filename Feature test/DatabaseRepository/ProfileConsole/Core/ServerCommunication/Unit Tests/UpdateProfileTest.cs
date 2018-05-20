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
    public class UpdateProfileTest
    {
        private string Username = "Fred5954";
        private string newDescription = "Bølle";
        private Tags tag;
        private ICollection<Tags> tags;
        IUpdateProfile _uut;
        private IUnitOfWork unitOfWork;

        [SetUp]
        public void Setup()
        {
            tag = new Tags {TagName = "Shingus"};
            tags = new Collection<Tags>() {tag};
            _uut = new UpdateProfile();
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        [Test]
        public void UpdateProfileInformation_Description_IsUpdated()
        {
            _uut.UpdateProfileInformation(Username, newDescription, tags);
            var person = unitOfWork.UserInformation.GetString(Username);

            Assert.That(person.Description, Is.EqualTo(newDescription));
        }

        [Test]
        public void UpdateProfileInformation_Taglist_IsUpdated()
        {
            _uut.UpdateProfileInformation(Username, newDescription, tags);

            var person = unitOfWork.UserInformation.GetString(Username);

            if (person.UserName == Username)
            {
                using (var db = new ProfileContext())
                {
                    var profile =
                        from p in db.UserInformation
                        where p.UserName == Username
                        select p;

                    foreach (var per in profile)
                    {
                        Assert.That(per.Tags, Is.EqualTo(tags));
                    }

                }

            }
        }
    }
}
