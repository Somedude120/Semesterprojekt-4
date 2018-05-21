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
//    public class LoginRequestTest
//    {
//        private string Username = "Bobby69";
//        private string IncorrectUsername = "Martent123";
//        private string Hash = "1234ABCDEFG";
//        private string IncorrectHash = "53452345";
//        private string CorrectLogin = "OK";
//        private string IncorrectInfo = "IncorrectLoginInfo";

//        private ILoginRequest _uut;
//        [SetUp]
//        public void Setup()
//        {
//            _uut = new LoginRequest();
//        }

//        [Test]
//        public void LoginRequest_Correct_LoginInfo()
//        {
//            Assert.That(_uut.Login(Username, Hash), Is.EqualTo(CorrectLogin));
//        }

//        [Test]
//        public void LoginRequest_Incorrect_Hash()
//        {
//            Assert.That(_uut.Login(Username, IncorrectHash), Is.EqualTo(IncorrectInfo));
//        }

//        [Test]
//        public void LoginRequest_Incorrect_Username()
//        {
//            Assert.That(_uut.Login(IncorrectUsername, Hash), Is.EqualTo(IncorrectInfo));
//        }
//    }
//}
