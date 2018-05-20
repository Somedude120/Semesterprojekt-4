using System;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using AsyncServer.Class;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.Core;

namespace Salt_And_Hash
{
    [TestFixture]
    public class SaltTest
    {
        private SaltedHash _uut1;
        private SaltedHash _uut2;
        private string _pwd;
        private string _sometext;

        [SetUp]
        public void Setup()
        {
            _uut1 = new SaltedHash("1234");
        }

        [Test]
        public void Verify_Salt()
        {
            bool right = _uut1.Verify(_uut1.Salt, _uut1.Hash, "1234");
            Assert.AreEqual(right, true);
        }

        [Test]
        public void Passwords_Are_Unique_Salt()
        {
            _uut1 = new SaltedHash("1234");
            _uut2 = new SaltedHash("1234");

            Assert.AreNotEqual(_uut2, _uut1);
        }

        [Test]
        public void Passwords_Are_Unique_Salt_Verify()
        {
            _uut1 = new SaltedHash("1234");
            _uut2 = new SaltedHash("1234");

            Console.WriteLine("Test1: " + _uut1.Hash);
            Console.WriteLine("Test2: " + _uut2.Hash);

            //Den tjekker om Hash passer sammen
            bool notSame = _uut2.Verify(_uut1.Salt, _uut2.Hash, "1234");
            Assert.That(notSame, Is.EqualTo(false));
        }

        [Test]
        public void Salt_And_Hash_Transforms_A_String()
        {
            string saltyPwd = "Godmode1";
            _uut1 = new SaltedHash(saltyPwd);

            Assert.AreNotEqual(saltyPwd, _uut1);
        }
    }
}
