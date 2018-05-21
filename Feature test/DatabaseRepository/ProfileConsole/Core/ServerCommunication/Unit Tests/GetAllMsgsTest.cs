using System.Collections.Generic;
using NUnit.Framework;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using ProfileConsole.Persistence;

namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
{
    [TestFixture]
    public class GetAllMsgsTest
    {
        private int GroupId = 1;
        private int MessageNumber1 = 1;
        private int MessageNumber2 = 2;
        private int MessageNumber3 = 3;
        private int MessageNumber4 = 4;
        private List<Chat> Messages;
        private string Sender = "AlexD";

        IGetAllMsgs _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new GetAllMsgs();
            Messages = new List<Chat>() { };
        }

        [Test]
        public void ReturnsCorrectMessages_For_GroupIdEquals1()
        {
            using (var unitOfWork = new UnitOfWork(new ProfileContext()))
            {
                var messages = unitOfWork.ChatGroup.GetChatWithChatGroups("Marto");
                Assert.That(messages.GroupId, Is.EqualTo(1));
                Assert.That(messages.Chat.Count, Is.EqualTo(4));
            }
        }
    }
}
