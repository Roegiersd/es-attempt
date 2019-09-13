using be.roegiersbvba.Customers.Domain.Events;
using System;



namespace be.roegiersbvba.Customers.Domain
{
    public static class AddressFactory
    {
        public static Address CreateAddressFromCommand(AggregateRoot root, AddressCreated e)
        {
            return new Address(root, e);
        }

        public static Address CreateAddressFromReplay(AggregateRoot root, string city, string zip, string street, string number, string function)
        {
            return new Address(root, city, zip, street, number, function);
        }


    }

    public sealed class Address : Entity
    {
        public Guid Id { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Number { get; private set; }
        public string Function { get; private set; }

        internal Address(AggregateRoot root, string city, string zip, string street, string number, string function) : base(root)
        {
            RegisterEventHandlers<AddressFunctionChanged>(OnFunctionChanged);
            RegisterEventHandlers<AddressCreated>(OnAddressCreated);
            if (string.IsNullOrEmpty(city)) throw new ArgumentException("message", nameof(city));
            Apply(new AddressCreated(Guid.NewGuid(), street, city, zip, number, function));
        }

        internal Address(AggregateRoot root, AddressCreated e) : base(root)
        {
            Apply(e);
        }



        public void ChangeAddressFunction(string function)
        {
            Apply(new AddressFunctionChanged(function));
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