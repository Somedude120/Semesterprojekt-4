using NSubstitute;
using NUnit.Framework;
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
{
    [TestFixture]
    class GetAllMsgsTest
    {
        private int GroupID = 1;
        private int MessageNumber = 1;
        private string Message = "Hello World";
        private string Sender = "AlexD";

        IGetAllMsgs _uut;
        IGetAllMsgs myChat;

        [SetUp]
        public void Setup()
        {
            myChat = Substitute.For<IGetAllMsgs>();
            _uut = new GetAllMsgs();
            myChat.groupId = GroupID;
            myChat.messageNumber = MessageNumber;
            myChat.message = Message;
            myChat.sender = Sender;
            

        }
    }
}
