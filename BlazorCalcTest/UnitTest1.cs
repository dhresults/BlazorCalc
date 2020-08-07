using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlazorCalc;

namespace BlazorCalcTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCalculate()
        {
            Assert.AreEqual(new Calculator("9", "1", "+").calculate(), "10");
            Assert.AreEqual(new Calculator("1", "7", "-").calculate(), "-6");
            Assert.AreEqual(new Calculator("8", "8", "*").calculate(), "64");
            Assert.AreEqual(new Calculator("12", "3", "/").calculate(), "4");
            Assert.AreEqual(new Calculator("256", "0", "/").calculate(), "Infinity");
            Assert.AreEqual(new Calculator("", "423", "+").calculate(), "423");
            Assert.AreEqual(new Calculator("foo", "14", "+").calculate(), "14");
        }
    }
}
