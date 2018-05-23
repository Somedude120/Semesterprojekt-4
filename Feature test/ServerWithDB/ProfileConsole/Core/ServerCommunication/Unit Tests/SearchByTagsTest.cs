//using NUnit.Framework;
//using ProfileConsole.Core.Domain;
//using System.Collections.Generic;

//namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
//{
//    class SearchByTagsTest
//    {
//        private string UserName = "AlexD";
//        private string TagName = "Kødbolle";
//        SearchByTags _uut;
//        Tags tags;

//        public List<string> FakeUserNameList;

//        [SetUp]
//        public void Setup()
//        {
//            _uut = new SearchByTags();
//            tags = new Tags();

//            FakeUserNameList = new List<string>{"AlexD"};
//        }

//        [Test]
//        public void Tag_Matches_UserName_Returns_Username()
//        {
//            var tagMatchReturnUserName = _uut.RequestTag(TagName);
//            Assert.That(tagMatchReturnUserName.UserInformation, Is.EqualTo(FakeUserNameList));
//        }
//    }
//}
