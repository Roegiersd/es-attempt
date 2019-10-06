using System;

namespace be.roegiersbvba.Customers.Domain
{
    public class MarkedAsPrimaryAddress : IEvent
    {
        public Guid Id { get; private set; }
    }

}