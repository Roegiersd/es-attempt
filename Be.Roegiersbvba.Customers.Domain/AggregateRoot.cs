using System;
using System.Collections.Generic;


namespace be.roegiersbvba.Customers.Domain
{
    public abstract class AggregateRoot
    {
        public readonly List<object> Events;
        protected Dictionary<Type, Action<object>> Eventhandlers;
        protected AggregateRoot()
        {
            Events = new List<object>();
            Eventhandlers = new Dictionary<Type, Action<object>>();
        }

        internal void Apply(IEvent e)
        {
            var handler = Eventhandlers[e.GetType()];
            handler.Invoke(e);
            Events.Add(e);
        }



        protected void RaiseEvent(IEvent @event)
        {
            Events.Add(@event);
        }


        internal void RegisterEventHandlers<T>(Action<T> handler) where T : IEvent
        {
            var eventType = typeof(T);
            if (!typeof(IEvent).IsAssignableFrom(eventType))
                throw new DomainException(string.Format("Error while trying to register eventtype {0}. Type not marked as IEvent.", eventType.ToString()));
            Eventhandlers.Add(eventType, e => handler((T)e));

        }

    }
}