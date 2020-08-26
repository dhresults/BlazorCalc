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
        [TestMethod]
        public void TestMultipleOperationsAndPrecedence()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(calc.evaluate("42+42*42-42/42"), "1805");
            Assert.AreEqual(calc.evaluate("1+2*3/4+5"), "7.5");
            Assert.AreEqual(calc.evaluate("0.5*1.5/0.25-1"), "2");
            Assert.AreEqual(calc.evaluate("0.025+12.5*15.25+2+3.5/10"), "193");
        }
    }
}

