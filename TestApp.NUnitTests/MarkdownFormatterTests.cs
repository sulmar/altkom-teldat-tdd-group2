using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.NUnitTests
{
    public class MarkdownFormatterTests
    {
        private MarkdownFormatter formatter;


        [SetUp]
        public void Setup()
        {
            this.formatter = new MarkdownFormatter();
        }

        [Test]
        [TestCase("a")]
        [TestCase("Lorem ipsum")]
        [TestCase("****")]
        public void FormatAsBold_ContentIsNotEmpty_ReturnWithDoubleAsterix(string content)
        {
            var result = formatter.FormatAsBold(content);

            // Specific test
            Assert.That(result, Is.EqualTo($"**{content}**"));


            StringAssert.StartsWith("**", result);

            // General test
            Assert.That(result, Does.StartWith("**"));
            Assert.That(result, Does.Contain(content));
            Assert.That(result, Does.EndWith("**"));

            result.Should()
                .StartWith("**")
                .And.Contain(content)
                .And.EndWith("**");

        }

        [Test]
        public void FormatAsBold_ContentUndefinied_ThrowArgumentNullException()
        {
            TestDelegate act = () => formatter.FormatAsBold(null);

            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
