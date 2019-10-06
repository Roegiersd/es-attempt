using System;
using System.Collections.Generic;
using System.Linq;

namespace be.roegiersbvba.Customers.Domain
{
    public class Customer : AggregateRoot
    {
        //public static Customer Replay(List<object> events)
        //{
        //    var customer = new Customer();

        //    foreach (var @event in events)
        //    {
        //        var x = @event as IEvent;
        //        customer.Replay(x);
        //    }
        //    return customer;
        //}

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
            Apply(new AddressAdded(Id, address.Id));

        }
        public void ChangePrimaryAddress(Guid identifier)
        {
            var oldPrimaryAddress = Addresses.FirstOrDefault(ad => ad.IsPrimaryAddress);
            var newPrimaryAddress = Addresses.FirstOrDefault(ad => ad.Id.Equals(identifier));
            if (newPrimaryAddress.IsPrimaryAddress)
                return;
            newPrimaryAddress.MarkAsPrimaryAddress();
            oldPrimaryAddress.CancelAsPrimaryAddress();
            Apply(new PrimaryAddressChanged(newPrimaryAddress, oldPrimaryAddress));

        }

        private Customer(IEvent @event) : this(@event.Id)
        {
            Apply(@event); //fix sidefx
        }

        private Customer() : this(Guid.NewGuid())
        {
        }

        private Customer(Guid id) : base()
        {
            Id = id;
            Addresses = new List<Address>();
            RegisterEventHandlers<CustomerCreated>(OnCustomerCreated, Id);
            RegisterEventHandlers<AddressAdded>(OnAddressAdded, Id);
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
            Apply(new CustomerCreated(name, firstname, customerType, Id));

        }

        private void OnPrimaryAddresschanged(PrimaryAddressChanged e)
        {
            this.PrimaryAddress = e.NewPrimaryAddress;
            var index = Addresses.IndexOf(this.Addresses.First(ad => ad.Id.Equals(e.NewPrimaryAddress.Id)));
            if (index != -1)
                Addresses[index] = e.NewPrimaryAddress;
            else
            {
                Addresses.Add(e.NewPrimaryAddress);
            }

            if (e.OldPrimaryAddress != null)
            {
                index = -1;
                index = Addresses.IndexOf(this.Addresses.First(ad => ad.Id.Equals(e.NewPrimaryAddress.Id)));
                if (index != -1)
                    Addresses[index] = e.OldPrimaryAddress;
                else
                {
                    Addresses.Add(e.OldPrimaryAddress);
                }
            }
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

            this.Addresses.Add(this.PopEntity<Address>(e.AddressId));
        }


    }




}
