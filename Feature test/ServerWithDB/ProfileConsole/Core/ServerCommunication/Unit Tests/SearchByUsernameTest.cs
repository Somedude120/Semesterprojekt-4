using System.Collections.Generic;
using System.Collections.ObjectModel;
using NSubstitute;
using NUnit.Framework;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
{
    [TestFixture]
    public class SearchByUsernameTest
    {
        private string Username = "Fred5954";
        private string Description = "Flot";
        private ICollection<Tags> tags;
        ISearchByUsername _uut;
        IOtherProfile profile;

       [SetUp]
        public void Setup()
        {
            tags = new Collection<Tags>();
            profile = Substitute.For<IOtherProfile>();
            _uut = new SearchByUsername();
            profile.username = Username;
            profile.description = Description;
            profile.tags = tags;

        }

        [Test]
        public void Username_IsInDB_ReturnsCorrectly()
        {
            
            Assert.That(_uut.RequestUsername(Username).Description, Is.EqualTo("Flot"));
        }

    }
}
