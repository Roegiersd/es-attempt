using System;

namespace be.roegiersbvba.Customers.Domain
{
    public abstract class DomainObject
    {

        protected Guid Identifier { get; set; }
    }
}
