using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.NUnitTests
{
    public class LoggerTests
    {
        private Logger logger;

        [SetUp]
        public void Setup()
        {
            logger = new Logger();
        }

        [Test]
        public void Log_CreateLogger_IsLastMessageIsNull()
        {
            // Assert
            Assert.That(logger.LastMessage, Is.Null);
        }

        [Test]
        public void Log_WhenMessageIsNotEmpty_SetLastMessage()
        {
            // Act
            logger.Log("a");

            // Assert
            Assert.That(logger.LastMessage, Is.EqualTo("a"));

            logger.LastMessage.Should().Be("a");
        }

        [Test]
        public void Log_WhenMessageIsEmpty_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(()=>logger.Log(null));
        }
    }
}
