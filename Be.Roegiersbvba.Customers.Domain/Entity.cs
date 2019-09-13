using System;

namespace be.roegiersbvba.Customers.Domain
{
    public abstract class Entity
    {
        private AggregateRoot Root;
        private Entity()
        {

        }

        protected void Apply(IEvent e) => Root.Apply(e);

        internal Entity(AggregateRoot root)
        {
            if (root == null)
                throw new DomainException("A non root entity cannot exist without an AggregateRoot");
            Root = root;
        }

        protected void RegisterEventHandlers<T>(Action<T> handler) where T : IEvent
        {
            var type = typeof(T);
            Root.RegisterEventHandlers<T>(handler);
        }


    }
}
