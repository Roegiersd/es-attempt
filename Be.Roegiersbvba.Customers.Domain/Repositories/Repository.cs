using System;
using System.Collections.Generic;
using System.Text;

namespace be.roegiersbvba.Customers.Domain.Repositories
{
    public static class Repository
    {
        static Repository()
        {
            Customers = new List<Customer>();
        }
        public static List<Customer> Customers { get; set; }
    }
}
