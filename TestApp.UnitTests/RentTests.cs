using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestApp.UnitTests
{
    [TestClass]
    public class RentTests
    {
        [TestMethod]
        public void Method_Scenario_ExpectedBehavior()
        {
            // Arrange

            // Act

            // Assert

        }

        [TestMethod]
        public void CanReturn_UserIsAdmin_ReturnTrue()
        {
            // Arrange
            var rent = new Rent(new User());

            // Act
            var result = rent.CanReturn(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void CanReturn_UserIsNotAdmin_ReturnFalse()
        {
            // Arrange
            var rent = new Rent(new User());

            // Act
            var result = rent.CanReturn(new User { IsAdmin = false });

            // Assert
            Assert.IsFalse(result);

        }


        [TestMethod]
        public void CanReturn_UserIsTheSameRentee_ReturnTrue()
        {
            // Arrange
          
            var user = new User();
            var rent = new Rent(user);

            // Act
            var result = rent.CanReturn(user);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        [Ignore("No bo tak")]
        // [ExpectedException(typeof(ArgumentNullException))]
        public void CanReturn_UserIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            var rent = new Rent(new User());

            // Act
            Action act = () => rent.CanReturn(null);

            // Assert
            var message = Assert.ThrowsException<ArgumentNullException>(act).Message;

            Assert.AreEqual("Value cannot be null. Parameter name: user", message);

        }
    }
}
