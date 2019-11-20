using FluentAssertions;
using NUnit.Framework;
using Shouldly;
using TestApp;

namespace Tests
{
    public class Tests
    {
        private MathCalculator mathCalculator;

        [SetUp]
        public void Setup()
        {
            mathCalculator = new MathCalculator();
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnGreaterArgument(int first, int second, int expected)
        {
            // Act
            var result = mathCalculator.Max(first, second);

            // Assert
            Assert.AreEqual(expected, result);

            Assert.That(result, Is.EqualTo(expected));

            // dotnet add package FluentAssertions

            result
                .Should()
                .Be(expected);

            // dotnet add package Shouldly

            result.ShouldBe(expected);


        }
        [Test]
        [TestCase(2, 1, ExpectedResult = 2)]
        [TestCase(1, 2, ExpectedResult = 2)]
        [TestCase(1, 1, ExpectedResult = 1)]
        public int Max_WhenCalled_ReturnGreaterArgument(int first, int second)
        {
            // Act
            return mathCalculator.Max(first, second);

        }

        [Ignore("No bo tak")]
        public void Test()
        {
            float x = 10;

            var mathCalculator = new MathCalculator();

            var result = mathCalculator.Add(1, 2);
        }

        private static int[] ExpectedPrimeNumbers =>
            new int[] { 2, 3, 5, 7, 11};

        [Test]
        public void GetPrimeNumbersBelow_LimitIs12_ReturnPrimeNumbersUpToLimit()
        {
            var result = mathCalculator.GetPrimeNumbersBelow(limit: 12);

            Assert.That(result, Is.Not.Empty);
          //  Assert.That(result, Has.Count.EqualTo(5));

            Assert.That(result, Does.Contain(2));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(ExpectedPrimeNumbers));

            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);

            // Fluent
            result.Should()
                .NotBeEmpty()
                .And
                .ContainInOrder(ExpectedPrimeNumbers);

            // Shouldly
            result
                .ShouldNotBeEmpty();


        }


    }
    
}