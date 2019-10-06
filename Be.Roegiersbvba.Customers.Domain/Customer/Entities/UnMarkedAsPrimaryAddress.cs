using System;

namespace be.roegiersbvba.Customers.Domain
{
    internal class UnMarkedAsPrimaryAddress : IEvent
    {
        public Guid Id { get; private set; }
    }
}