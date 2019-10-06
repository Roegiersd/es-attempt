using System;

namespace be.roegiersbvba.Customers.Domain
{
    internal sealed class AddressAdded : IEvent
    {
        public AddressAdded(Guid id, Guid addressId)
        {
            Id = id;
            AddressId = addressId;
        }
      
        public Guid Id { get; private set; }
        public Guid AddressId { get; private set; }
      
    }
}