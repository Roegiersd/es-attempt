using System;

namespace be.roegiersbvba.Customers.Domain
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}