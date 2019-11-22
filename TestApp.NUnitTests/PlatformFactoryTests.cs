using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Fundamentals;

namespace TestApp.NUnitTests
{
    public class PlatformFactoryTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void Down()
        {

        }

        [Test]
        public void Create_UfoArgument_ThrowNotSupportedException()
        {

        }

        [Test]
        public void Create_FriendICAO_ReturnFriend()
        {
            // Act
            var platform = PlatformFactory.Create("SP");

            // Assert
            Assert.That(platform, Is.TypeOf<Friend>());
            Assert.That(platform, Is.InstanceOf<Platform>());
        }


        [TestCase("ES", "Estonia")]
        [TestCase("D", "Niemcy")]
        [TestCase("9A", "Chorwacja")]
        [TestCase("S5", "Słowenia")]
        public void Create_FoeICAO_ReturnFoe(string icao)
        {
            // Act
            var platform = PlatformFactory.Create(icao);

            // Assert
            Assert.That(platform, Is.TypeOf<Foe>());
        }

        [TestCaseSource("ICAOs")]
        public void Create_FoeICAO_ReturnFoe2(string icao)
        {
            // Act
            var platform = PlatformFactory.Create(icao);

            // Assert
            Assert.That(platform, Is.TypeOf<Foe>());
        }

        // https://datahub.io/collections
        static object[] ICAOs =
        {
            new object[] { "ES", "Estonia" },
            new object[] { "D", "Niemcy" },
            new object[] { "9A", "Chorwacja" },
            new object[] { "S5", "Słowenia" }
        };
    }
}
