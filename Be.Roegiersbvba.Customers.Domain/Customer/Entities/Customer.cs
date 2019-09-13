using System;
using System.Collections.Generic;
using System.Linq;
using be.roegiersbvba.Customers.Domain.Events;

namespace be.roegiersbvba.Customers.Domain
{
    public class Customer : AggregateRoot
    {
        public static Customer Replay(List<object> events)
        {
            var customer = new Customer();
            foreach (var @event in events)
            {
                var x = @event as IEvent;
                customer.Apply(x);
            }
            return customer;
        }

        public string Name { get; private set; }
        public string Firstname { get; private set; }
        public string CustomerType { get; private set; }
        public List<Address> Addresses { get; private set; }
        public Address PrimaryAddress { get; private set; }
        public Address DefaultShipTo { get; private set; }
        public List<Contact> Contacts { get; private set; }

        public Guid Id { get; private set; }

        public void AddAddress(string street, string city, string zip, string number, string function)
        {
            var address = new Address(this, city, zip, street, number, function);
            Apply(new AddressAdded(address));

        }
        public void ChangePrimaryAddress(Address newPrimaryAddress)
        {
            if (Addresses.Any(x => x.Id == newPrimaryAddress.Id))
            {
                this.PrimaryAddress = newPrimaryAddress;
                //raiseevent
            }
            throw new DomainException();
        }

        private Customer() : base()
        {
            Addresses = new List<Address>();
            RegisterEventHandlers<CustomerCreated>(OnCustomerCreated);
            RegisterEventHandlers<AddressAdded>(OnAddressAdded);
            
        }

        public Customer(string name, string firstname, string customerType) : this()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrEmpty(firstname))
            {
                throw new ArgumentException("message", nameof(firstname));
            }

            if (string.IsNullOrEmpty(customerType))
            {
                throw new ArgumentException("message", nameof(customerType));
            }
            Apply(new CustomerCreated(name, firstname, customerType, Guid.NewGuid()));

        }

        private void OnCustomerCreated(CustomerCreated e)
        {
            this.Name = e.Name;
            this.Firstname = e.Firstname;
            this.CustomerType = e.CustomerType;
            this.Id = e.Id;
        }

        private void OnAddressAdded(AddressAdded e)
        {
            this.Addresses.Add(e.Address);
        }

        
    }


    

}
