using Microsoft.VisualStudio.TestTools.UnitTesting;
using q4;
using System;
using System.Collections.Generic;
using System.Text;

namespace q4.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void validPassTest()
        {
            Assert.AreEqual(true, Program.validPass("ff3fgrHFTH"));
            Assert.AreEqual(false, Program.validPass("dde"));
        }
    }
}