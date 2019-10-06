using System;
using be.roegiersbvba.Customers.Domain.Events;

namespace be.roegiersbvba.Customers.Domain
{
    internal class CustomerCreated : EventBase<Customer>, ICreatedAggregate
    {
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string CustomerType { get; set; }


        public CustomerCreated(string name, string firstname, string customerType, Guid id) : base()
        {
            this.Name = name;
            this.Firstname = firstname;
            this.CustomerType = customerType;
            this.Id = id;
        }
    }
}