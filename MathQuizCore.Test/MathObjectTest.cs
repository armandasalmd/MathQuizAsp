using MathQuizCore.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathQuizCore.Test
{
    [TestClass]
    public class MathObjectTest
    {

        [TestMethod]
        public void SingleDigitNumber()
        {
            // Arrange
            MathObject objToTest = new MathObject(2);
            // Act
            string result = objToTest.ToString();
            // Assert
            Assert.AreEqual("2", result);
        }

        [TestMethod]
        public void DoubleDigitNumber()
        {
            // Arrange
            MathObject objToTest = new MathObject(233);
            // Act
            string result = objToTest.ToString();
            // Assert
            Assert.AreEqual("233", result);
        }

        [TestMethod]
        public void ConvertsMathSymbolEnumToString()
        {
            // Arrange
            MathObject objToTest = new MathObject(Operator.Multiply);
            // Act
            string result = objToTest.ToString();
            // Assert
            Assert.AreEqual("*", result);
        }
    }
}
