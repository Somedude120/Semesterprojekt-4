using System.Collections.Generic;
using NUnit.Framework;

namespace AsyncServerHashing.UnitTest
{
    [TestFixture]
    public class HashingUnitTest
    {
        private AsynchronousClient _client;


        [SetUp]
        public void Setup()
        {
            _client = new AsynchronousClient();
        }

        [Test]
        public void TestStartCommand()
        {
        }
    }
}