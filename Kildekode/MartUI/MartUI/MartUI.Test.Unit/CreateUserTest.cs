using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace MartUI.Test.Unit
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CreateUserTest
    {
        private ApplicationUnderTest _uut;
        private string _path;
        private UIMap map;

        public TestContext TestContext { get; set; }
        public UIMap UIMap => map ?? (map = new UIMap());


        [TestInitialize]
        public void Initialize()
        {
            _uut = ApplicationUnderTest.Launch(_path);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _uut.Close();
        }

        public CreateUserTest()
        {
            _path = "../../../BMI/bin/Debug/BMI.exe";
            Assert.IsTrue(File.Exists(_path));
        }

        #region Initial values

        [TestMethod]
        public void Initial_NoInput_NameIsCorrect()
        {

        }


        #endregion
        [TestMethod]
        public void CodedUITestMethod1()
        {
            UIMap.SetNameToGoodUsername();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.

        }
    }
}
