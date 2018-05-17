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
using ProfileConsole.Core.Domain;
using ProfileConsole.Core.ServerCommunication;
using ProfileConsole.Core.ServerCommunication.Interfaces;

namespace ProfileConsole.Core.ServerCommunication.Unit_Tests
{
    [TestFixture]
    public class LogoutTest
    {
        private string Username = "Fred5954";
        ILogout _uut;
        private IUnitOfWork unitOfWork;
        [SetUp]
        public void Setup()
        {
            _uut = new Logout();
            unitOfWork = new UnitOfWork(new ProfileContext());
        }

        [Test]
        public void Logout_SetsStatus_ToOffline()
        {
            _uut.LogoutDB(Username);
            var person = unitOfWork.UserInformation.GetString(Username);
            using (var db = new ProfileContext())
            {
                var profile =
                    from p in db.UserInformation
                    where p.UserName == Username
                    select p;

                foreach (var pers in profile)
                {
                    Assert.That(pers.Status, Is.EqualTo("Offline"));
                }
            }
        }


    }
}
