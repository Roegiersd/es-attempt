using System;
using System.Collections.Generic;

namespace be.roegiersbvba.Customers.Domain
{
    public abstract class ValueObjectBase<T> : IValueObject
        where T : ValueObjectBase<T>
    {
        //public AggregateRoot Root { get; private set; }
        ////public readonly List<object> Events;
        //protected ValueObjectBase(AggregateRoot root)
        //{
        //    Root = root;
        //    Events = new List<object>();

        //}

        //internal void Apply(IEvent e)
        //{
        //    Root.Apply(e);
        //}



        //protected void RaiseEvent(IEvent @event)
        //{
        //    Events.Add(@event);
        //}

        //A handler is registered for a specific entity (identifier), for simplicity guid.empty is used if no id is relevant.
        //internal void RegisterEventHandlers<T>(Action<T> handler, Guid id) where T : IEvent
        //{
        //    Root.RegisterEventHandlers(handler, id);
        //}



        //public override bool Equals(object obj)
        //{
        //    var valueObject = obj as T;

        //    if (ReferenceEquals(valueObject, null))
        //        return false;

        //    if (GetType() != obj.GetType())
        //        return false;

        //    return EqualsCore(valueObject);
        //}

        //protected abstract bool EqualsCore(T other);

        //public override int GetHashCode()
        //{
        //    return GetHashCodeCore();
        //}

        //protected abstract int GetHashCodeCore();

        //public static bool operator ==(ValueObjectBase<T> a, ValueObjectBase<T> b)
        //{
        //    if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
        //        return true;

        //    if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
        //        return false;

        //    return a.Equals(b);
        //}

        //public static bool operator !=(ValueObjectBase<T> a, ValueObjectBase<T> b)
        //{
        //    return !(a == b);
        //}
    }
}
