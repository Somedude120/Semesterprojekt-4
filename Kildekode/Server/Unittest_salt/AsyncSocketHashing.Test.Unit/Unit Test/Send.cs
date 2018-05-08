using System;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using AsyncServer.Class;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.Core;

namespace AsyncSocketHashing.Test.Unit.Unit_Test
{
    [TestFixture]
    public class Send
    {
        private AsynchronousSocketListener _uut;
        private StateObject _stateObject;
        private IAsyncResult _ar;

        [SetUp]
        public void Setup()
        {
            _uut = new AsynchronousSocketListener();
            _stateObject = Substitute.For<StateObject>();
            _ar = Substitute.For<IAsyncResult>();

        }

        [Test]
        public void Listening_For_Socket_Message()
        {
            

        }
    }
}