using System;

namespace be.roegiersbvba.Customers.Domain.Events
{
    public abstract class EventBase<ForT> : IEvent
    where ForT : class //entity or prop?
    {
        public DateTime Timestamp { get; private set; }
        public Guid Id { get; protected set; } //todo -> difference between entity id & event id.
        protected EventBase()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
