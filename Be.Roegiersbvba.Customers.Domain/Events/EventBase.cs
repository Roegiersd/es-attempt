using System;
using System.Collections.Generic;
using System.Text;

namespace be.roegiersbvba.Customers.Domain.Events
{
    public abstract class EventBase : IEvent
    {
        public DateTime Timestamp { get; private set; }

        protected EventBase()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
