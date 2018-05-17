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
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Tests.Unit
{
    [TestFixture]
    public class SearchByUsernameTest
    {
        private string Username = "Fred5954";
        private string Description = "Flot";
        private ICollection<Tags> tags;
        ISearchByUsername _uut;
        private OtherProfile profile;
       [SetUp]
        public void Setup()
        {
            tags = new Collection<Tags>();
            OtherProfile profile = new OtherProfile(Username, Description, tags);
            _uut = new SearchByUsername();
        }

        [Test]
        public void Username_IsInDB_ReturnsCorrectly()
        {

            Assert.That(_uut.RequestUsername(Username).username, Is.EqualTo(profile.username));
        }

    }
}
