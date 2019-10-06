using System;

namespace be.roegiersbvba.Customers.Domain.Events
{
   public sealed class AddressCreated : EventBase<Address>, ICreatedEntity
    {


        public AddressCreated(Guid id, string street, string city, string zipCode, string number, string function) : base()
        {
            Id = id;
            Street = street;
            City = city;
            ZipCode = zipCode;
            Number = number;
            Function = function;
        }

        //  public Guid Id { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Number { get; private set; }
        public string Function { get; private set; }
    }
}
