using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileConsole;
using ProfileConsole.Persistence;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NSubstitute;
using ProfileConsole.Core;
using ProfileConsole.Core.ServerCommunication;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
{
    [TestFixture]
    public class SaveMessagesTest
    {
        SaveMessages _uut;
        string sender =  "Fred5954";
        private string receiver = "Bobby69";
        private string message = "Dumme pringle";
        [SetUp]
        public void Setup()
        {
            _uut = new SaveMessages();
        }

        [Test]
        public void NewMessageIsSavedInDB()
        {
            SaveMessages.SaveIncomingMessage(receiver, sender, message);
            
        }
    }
}
