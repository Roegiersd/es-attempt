using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using be.roegiersbvba.Customers.Domain.Events;

namespace be.roegiersbvba.Customers.Domain
{
    public abstract class AggregateRoot
    {
        private Dictionary<Guid, object> CreatedEntityReferences = new Dictionary<Guid, object>();
        public Guid Id { get; protected set; }
        /// <summary>
        /// Required to apply non root entities to the aggregate during replay.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        internal T PopEntity<T>(Guid id)
        where T : Entity
        {
            object o = null;
            if (CreatedEntityReferences.ContainsKey(id))
                CreatedEntityReferences.Remove(id, out o);
            return (T)o;
        }
        public readonly List<object> Events;
        protected Dictionary<Type, Dictionary<Guid, Action<object>>> Eventhandlers;
        protected AggregateRoot(Guid id)
        {
            Id = id;
            Events = new List<object>();
            Eventhandlers = new Dictionary<Type, Dictionary<Guid, Action<object>>>();
            RegisterAllHandlers();
        }

        internal void Apply(IEvent e)
        {
            if (Eventhandlers.ContainsKey(e.GetType()) && Eventhandlers[e.GetType()].ContainsKey(e.Id))
            {
                var handler = Eventhandlers[e.GetType()][e.Id];
                handler.Invoke(e);
                Events.Add(e);

                if (e is ICreatedEntity)
                {
                    BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public |
                                             BindingFlags.NonPublic
                                             | BindingFlags.Static;
                    FieldInfo field = handler.Target.GetType().GetField("handler", bindFlags);
                    dynamic y = field.GetValue(handler.Target);
                    CreatedEntityReferences.Add(e.Id, y.Target);
                }
            }
            else
            {
                throw new DomainException(string.Format("No valid handler found for `{0}`", e.GetType().ToString()));
            }
        }

        internal void Replay(IEvent e)
        {
            if (e == null)
                throw new DomainException(string.Format("{0} cannot be null.", nameof(e)));
            if (Eventhandlers.ContainsKey(e.GetType()) && Eventhandlers[e.GetType()].ContainsKey(e.Id))
            {
                var handler = Eventhandlers[e.GetType()][e.Id];
                handler.Invoke(e);
                Events.Add(e);
            }
            else if (e is ICreatedEntity)
            {
                var type = e.GetType();
                Type baseType = type.BaseType;
                Type generic = typeof(EventBase<>);

                while (baseType != null && baseType != typeof(object))
                {
                    var cur = baseType.IsGenericType ? baseType.GetGenericTypeDefinition() : baseType;
                    if (generic == cur)
                    {
                        break;
                    }
                    baseType = baseType.BaseType;
                }
                if (baseType != null)
                {
                    var entityType = baseType.GetGenericArguments()[0];

                    //if (entityType.Equals(typeof(Entity)))  //FIX
                    //{
                    ConstructorInfo c = entityType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
                            null, new Type[] { typeof(AggregateRoot), typeof(Guid) }, null);
                    var entity = c.Invoke(new Object[] { this, e.Id });
                    Apply(e);
                    //}
                }
                else
                {
                    throw new DomainException("Unexpected path.");
                }
            }
            else
            {
                throw new DomainException("No valid handler found for to be completed..."); ///todo 
            }
        }

        /// <summary>
        /// This will probably also be the point where the snapshot is implemented.
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public static AggregateRoot Replay(List<object> events)
        {
            if (events == null || events.Count == 0)
                throw new DomainException("Cannot reconstitute aggregate from list without events");
            var firstEvent = events.First();
            AggregateRoot root = null;
            if (firstEvent is ICreatedAggregate)
            {
                var rootType = firstEvent.GetType().BaseType.GetGenericArguments()[0];
                ConstructorInfo c = rootType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
                    null, new Type[] { typeof(IEvent) }, null);
                root = (AggregateRoot)c.Invoke(new Object[] { firstEvent });
                events.Remove(firstEvent);
            }

            foreach (var @event in events)
            {
                root.Replay((IEvent)@event);

            }
            return root;
        }

        protected void RaiseEvent(IEvent @event)
        {
            Events.Add(@event);
        }

        //A handler is registered for a specific entity (identifier), for simplicity guid.empty is used if no id is relevant.
        internal void RegisterEventHandlers<T>(Action<T> handler, Guid id) where T : IEvent
        {
            var eventType = typeof(T);
            if (!typeof(IEvent).IsAssignableFrom(eventType))
                throw new DomainException(string.Format("Error while trying to register eventtype {0}. Type not marked as IEvent.", eventType.ToString()));
            if (Eventhandlers.ContainsKey(eventType))
            {
                Eventhandlers[eventType].Add(id, e => handler((T)e));
            }
            else
            {
                var handlers = new Dictionary<Guid, Action<object>>();
                handlers.Add(id, e => handler((T)e));
                Eventhandlers.Add(eventType, handlers);
            }
        }

        protected abstract void RegisterAllHandlers();


    }
}