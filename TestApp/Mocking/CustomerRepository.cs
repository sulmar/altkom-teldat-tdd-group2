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


    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }
    }
}
