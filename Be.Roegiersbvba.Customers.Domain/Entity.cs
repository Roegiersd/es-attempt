using System;
using System.Collections.Generic;

namespace be.roegiersbvba.Customers.Domain
{
    public abstract class Entity
    {
        public AggregateRoot Root { get; private set; }
        public readonly List<object> Events;
        protected Entity(AggregateRoot root)
        {
            Root = root;
            Events = new List<object>();

        }

        internal void Apply(IEvent e)
        {
            Root.Apply(e);
        }



        protected void RaiseEvent(IEvent @event)
        {
            Events.Add(@event);
        }

        //A handler is registered for a specific entity (identifier), for simplicity guid.empty is used if no id is relevant.
        internal void RegisterEventHandlers<T>(Action<T> handler, Guid id) where T : IEvent
        {
            Root.RegisterEventHandlers(handler, id);
        }
    }
}
