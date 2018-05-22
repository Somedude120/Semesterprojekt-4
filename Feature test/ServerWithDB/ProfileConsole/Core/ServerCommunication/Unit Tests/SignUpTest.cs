//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using NSubstitute;
//using NUnit.Framework;
//using ProfileConsole.Core.Domain;
//using ProfileConsole.Core.ServerCommunication.Interfaces;

//namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
//{
//    [TestFixture]
//    public class SignUpTest
//    {
//        ISignUp _uut;
//        private string ExistingUsername = "Fred5954";
//        private string NewUsername = "Bobby69";
//        private string Salt = "ABCDEFG1234";
//        private string Hash = "1234ABCDEFG";
//        private string Error = "Username already exists";
//        private string OK = "OK";

//        [SetUp]
//        public void Setup()
//        {
//            _uut = new SignUp(); 
//        }

//        [Test]
//        public void CreateProfile_UsernameAlreadyExists()
//        {
//            Assert.That(_uut.CreateProfile(ExistingUsername, Salt, Hash), Is.EqualTo(Error));
//        }

//        [Test]
//        public void CreateProfile_UserNameDoesntExist_OKMessage()
//        {

//            Assert.That(_uut.CreateProfile(NewUsername, Salt, Hash), Is.EqualTo(OK));
//        }

//    }
//}
