using be.roegiersbvba.Customers.Domain.Events;
using System;

namespace be.roegiersbvba.Customers.Domain
{
    public sealed class Address : Entity
    {
        public Guid Id { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Number { get; private set; }
        public string Function { get; private set; }
        public bool IsPrimaryAddress { get; private set; }

        internal Address(AggregateRoot root, string city, string zip, string street, string number, string function) : this(root, Guid.Empty)
        {
            if (string.IsNullOrEmpty(city)) throw new ArgumentException("message", nameof(city));
            Apply(new AddressCreated(Id, street, city, zip, number, function));
        }

        private Address(AggregateRoot root, Guid id) : base(root)
        {
            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
                this.Id = id;
            }

            RegisterEventHandlers<AddressFunctionChanged>(OnFunctionChanged, id);
            RegisterEventHandlers<AddressCreated>(OnAddressCreated, id);
            RegisterEventHandlers<MarkedAsPrimaryAddress>(OnMarkedAsPrimaryAddress, id);
            RegisterEventHandlers<UnMarkedAsPrimaryAddress>(OnPrimaryAddressCancelled, id);
        }

        private Address(AggregateRoot root, AddressCreated e) : this(root, e.Id)
        {
            Apply(e);
        }


        public Address MarkAsPrimaryAddress() // --> no event sourcing.
        {
            if (!String.IsNullOrEmpty((Street)))
                Apply(new MarkedAsPrimaryAddress());
            return this;
        }

        public Address CancelAsPrimaryAddress()
        {
            Apply(new UnMarkedAsPrimaryAddress());
            return this;
        }

        public void ChangeAddressFunction(string function)
        {
            Apply(new AddressFunctionChanged(function));
        }

        public void OnMarkedAsPrimaryAddress(MarkedAsPrimaryAddress e)
        {
            this.IsPrimaryAddress = true;
        }

        internal void OnPrimaryAddressCancelled(UnMarkedAsPrimaryAddress e)
        {
            this.IsPrimaryAddress = false;
        }
        public void OnFunctionChanged(AddressFunctionChanged e)
        {
            Function = e.Function;
        }
        public void OnAddressCreated(AddressCreated e)
        {

            City = e.City;
            Street = e.Street;
            ZipCode = e.ZipCode;
            Number = e.Number;
            Function = e.Function;
            Id = e.Id;
        }


    }
}