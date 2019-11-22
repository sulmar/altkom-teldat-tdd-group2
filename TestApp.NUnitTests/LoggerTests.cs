using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

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

        [Test]
        public void Log_WhenCalled_RaiseMessageLoggedEvent()
        {
            // Arrange
            var loggedDate = DateTime.MinValue;

            logger.MessageLogged += (sender, arg) => loggedDate = arg;

            // Act
            logger.Log("a");

            // Assert
            Assert.That(loggedDate, Is.Not.EqualTo(DateTime.MinValue));

        }

        [Test]
        public void Log_WhenCalled_RaiseMessageLoggedEvent2()
        {
            using (var monitoredLogger = logger.Monitor())
            {
                // Act
                logger.Log("a");

                // Assert
                monitoredLogger.Should().Raise(nameof(Logger.MessageLogged));
            }

        }

        [Test]
        public async Task LogAsync_WhenCalled_SetLastMessage()
        {
            // Act
            Func<Task> act = () => logger.LogAsync("a");
            
            await act.Should().CompleteWithinAsync(2.Seconds());

            // Assert
            Assert.That(logger.LastMessage, Is.EqualTo("a"));

        }
    }
}
