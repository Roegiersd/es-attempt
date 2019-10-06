using System;

namespace be.roegiersbvba.Customers.Domain
{
    internal class PrimaryAddressChanged : IEvent
    {
        public PrimaryAddressChanged(Address newPrimaryAddress, Address oldPrimaryAddress)
        {
            this.NewPrimaryAddress = newPrimaryAddress;
            this.OldPrimaryAddress = oldPrimaryAddress;
        }
        public Guid Id { get; private set; }
        public Address NewPrimaryAddress { get; }

        public Address OldPrimaryAddress { get; }
    }
}