using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.UnitTests
{
    [TestClass]
    public class MathCalculatorTests
    {
        private MathCalculator mathCalculator;

        [TestInitialize]
        public void Setup()
        {
            mathCalculator = new MathCalculator();
        }

        [TestMethod]
        public void Add_WhenCalled_ReturnTheSumOfArgument()
        {
            // Act
            var result = mathCalculator.Add(1, 2);

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Add_WhenMaxParameters_ReturnTheSum()
        {
            // Act
            Action act = () => mathCalculator.Add(int.MaxValue, int.MaxValue);

            // Assert
            Assert.ThrowsException<OverflowException>(act);

        }

        [TestMethod]
        public void Max_FirstArgumentIsGreaterThanSecondArgument_ReturnFirstArgument()
        {
            // Act
            var result = mathCalculator.Max(2, 1);

            // Assert
            Assert.AreEqual(2, result);

        }

        [TestMethod]
        public void Max_SecondArgumentIsGreaterThanFirstArgument_ReturnSecondArgument()
        {
            // Act
            var result = mathCalculator.Max(1, 2);

            // Assert
            Assert.AreEqual(2, result);

        }

        [TestMethod]
        public void Max_FirstAndSecondArgumentAreEqual_ReturnsTheSameArgument()
        {
            // Act
            var result = mathCalculator.Max(1, 1);

            // Assert
            Assert.AreEqual(1, result);
        }
        

        [DataTestMethod]
        [DataRow(2, 1, 2)]
        [DataRow(1, 2, 2)]
        [DataRow(1, 1, 1)]
        public void Max_WhenCalled_ReturnGreaterArgument(int first, int second, int expected)
        {
            // Act
            var result = mathCalculator.Max(first, second);

            // Assert
            Assert.AreEqual(expected, result);

        }
    }
}
