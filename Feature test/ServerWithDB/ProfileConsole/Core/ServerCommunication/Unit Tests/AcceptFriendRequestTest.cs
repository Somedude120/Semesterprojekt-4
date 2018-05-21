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
    public class AcceptFriendRequestTest
    {
        private IAcceptFriendRequest _uut;
        private string Username = "Bobby69";
        private string FriendRequestSender = "Fred5954";

        [SetUp]
        public void Setup()
        {
            _uut = new AcceptFriendRequest();
        }

        [Test]
        public void AcceptFriendRequest_FunctionCorrectly()
        {
            _uut.AcceptRequest(Username, FriendRequestSender);
            
        }

    }
}
