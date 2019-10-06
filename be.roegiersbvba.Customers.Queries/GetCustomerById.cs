using System;

namespace be.roegiersbvba.Customers.Queries
{
    public class GetCustomerById : IQuery
    {
        public Guid Id { get; set; }
    }
}
