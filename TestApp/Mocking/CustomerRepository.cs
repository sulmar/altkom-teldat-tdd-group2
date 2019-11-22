using Bogus;
using System;
using System.Collections.Generic;

namespace TestApp.Fakers
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRemoved { get; set; }
        public Gender Gender { get; set; }
        public decimal Salary { get; set; }
        public float Weight { get; set; }
    }

    public enum Gender
    {
        Male,
        Fale
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }

    
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
    }


    public class FakeCustomerRepository : ICustomerRepository
    {
        private IEnumerable<Customer> customers;

        public FakeCustomerRepository()
        {
            CustomerFaker customerFaker = new CustomerFaker();

            customers = customerFaker.Generate(100);
        }
        
        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }
    }

    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Gender, f => (Gender)f.Person.Gender);
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.3f));
            RuleFor(p => p.Salary, f => f.Random.Decimal(1000, 2000));
            RuleFor(p => p.Weight, f => f.Random.Float(50, 150));
        }
    }

    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }
    }
}
