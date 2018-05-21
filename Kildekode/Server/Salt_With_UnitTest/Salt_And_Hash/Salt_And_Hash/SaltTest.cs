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

        }

        [Test]
        public void SaltedHash_Salts_Are_Not_Same()
        {
            _uut1 = new SaltedHash();
            string salt1 = _uut1.MakeSalt();

            string salt2 = _uut1.MakeSalt();

            Assert.AreNotEqual(salt1, salt2);

        }

        [Test]
        public void SaltedHash_Hashes_Are_The_Same()
        {
            _uut1 = new SaltedHash();

            string pwd = "derp";
            string hash1 = _uut1.GetStringSha256Hash(pwd);
            string hash2 = _uut1.GetStringSha256Hash(pwd);
            Console.WriteLine($"Hashed Pass1: {hash1}\nHashed Pass2: {hash2}");
            Assert.AreEqual(hash1, hash2);
        }
        [Test]
        public void SaltedHash_Pass_Are_Not_Same()
        {
            _uut1 = new SaltedHash();

            string pwd1 = "derp";
            string pwd2 = "Godmode1";
            string hash1 = _uut1.GetStringSha256Hash(pwd1);
            string hash2 = _uut1.GetStringSha256Hash(pwd2);
            Console.WriteLine($"Hashed Pass1: {hash1}\nHashed Pass2: {hash2}");
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void SaltedHash_ComputeHashes_Are_Not_Same()
        {
            _uut1 = new SaltedHash();
            string salt1 = _uut1.MakeSalt();
            string salt2 = _uut1.MakeSalt();
            string pwd = "Godmode1";



            Assert.AreNotEqual(_uut1.ComputeHash(salt1, pwd), _uut1.ComputeHash(salt2, pwd));
        }
    }
}
