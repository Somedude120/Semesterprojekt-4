//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ProfileConsole;
//using ProfileConsole.Persistence;
//using NUnit.Framework;
//using NUnit.Framework.Internal;
//using NSubstitute;
//using ProfileConsole.Core.Domain;
//using ProfileConsole.Core.ServerCommunication;
//using ProfileConsole.Core.ServerCommunication.Interfaces;

//namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
//{
//    [TestFixture]
//    public class GetFriendsTest
//    {
//        private string Username = "Fred5954";
//        private string ExpectedFriendUserName = "AlexD";
//        IGetFriends _uut;
        
//        [SetUp]
//        public void Setup()
//        {
//            _uut = new GetFriends();
            
//        }

//        [Test]
//        public void GetFriendList_ListIsReturnedCorrectly()
//        {
//            Assert.That(_uut.GetFriendList(Username)[0], Is.EqualTo(ExpectedFriendUserName));
//        }
//    }
//}
