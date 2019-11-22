using Bogus;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Fakers;

namespace TestApp.NUnitTests
{
    public class CustomerRepositoryTests
    {
        private ICustomerRepository customerRepository;


        [SetUp]
        public void Setup()
        {
            customerRepository = new FakeCustomerRepository();
        }

        [Test]
        public void Get_WhenCalled_ReturnCustomers()
        {
            // Act
            var result = customerRepository.Get();

            // Assert


        }

        [Test]
        public void Add_Customer_Saved()
        {
            var customer = new Faker<Customer>()
                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                .RuleFor(p => p.LastName, f => f.Person.LastName)
                .Generate();
        }
    }
}
