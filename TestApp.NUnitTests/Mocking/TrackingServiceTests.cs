using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Mocking;

namespace TestApp.NUnitTests.Mocking
{
    public class FakeEmptyFile : IFileReader
    {
        public string ReadAllText(string path)
        {
            return string.Empty;
        }
    }

    public class FakeValidFile : IFileReader
    {
        public string ReadAllText(string path)
        {
            Location location = new Location(53.125, 18.011111);

            return JsonConvert.SerializeObject(location);
        }
    }

    public class FakeInvalidFile : IFileReader
    {
        public string ReadAllText(string path)
        {
            return "a";
        }
    }

    public class TrackingServiceTests
    {
        private Mock<IFileReader> mockFileReader;
        private TrackingService trackingService;

        [SetUp]
        public void Setup()
        {
            mockFileReader = new Mock<IFileReader>();
            trackingService = new TrackingService(mockFileReader.Object);
        }

        [Test]
        public void Get_ValidJson_ReturnLocation()
        {
            // Arrange
            IFileReader fileReader = new FakeValidFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            var result = trackingService.Get();

            // Assets
            Assert.IsNotNull(result);
            Assert.That(result.Latitude, Is.EqualTo(53.125));
            Assert.That(result.Longitude, Is.EqualTo(18.011111));
        }

        [Test]
        public void Get_ValidJson_ReturnLocation2()
        {
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(new Location(53.125, 18.011111)));


            // Act
            var result = trackingService.Get();

            // Assets
            Assert.IsNotNull(result);
            Assert.That(result.Latitude, Is.EqualTo(53.125));
            Assert.That(result.Longitude, Is.EqualTo(18.011111));
        }


        [Test]
        public void Get_EmptyFile_ThrowApplicationException()
        {
            // Arrange
            IFileReader fileReader = new FakeEmptyFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            TestDelegate act = ()=> trackingService.Get();

            // Assets
            Assert.Throws<ApplicationException>(act);

        }

        [Test]
        public void Get_EmptyFile_ThrowApplicationException2()
        {
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(string.Empty);

            // Act
            TestDelegate act = () => trackingService.Get();

            // Assets
            Assert.Throws<ApplicationException>(act);

        }





        [Test]
        public void Get_InvalidJson_ThrowApplicationException()
        {
            // Arrange
            IFileReader fileReader = new FakeInvalidFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            TestDelegate act = () => trackingService.Get();

            // Assets
            Assert.Throws<ApplicationException>(act);
        }

        [Test]
        public void Get_InvalidJson_ThrowApplicationException2()
        {
            mockFileReader.Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns("a");

            // Act
            TestDelegate act = () => trackingService.Get();

            // Assets
            Assert.Throws<ApplicationException>(act);
        }


    }
}
