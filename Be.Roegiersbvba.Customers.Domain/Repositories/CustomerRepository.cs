using System;
using System.Collections.Generic;


namespace be.roegiersbvba.Customers.Domain.Repositories
{
    public static class CustomerRepository
    {
        static CustomerRepository()
        {
            Customers = new Dictionary<Guid, List<object>>();
        }
        private static Dictionary<Guid, List<object>> Customers;

        public static List<object> GetCustomers(Guid id)
        {
            if (Customers.ContainsKey(id))
                return Customers[id];
            else
            {
                return null;
            }
        }

        public static void SaveCustomer(Customer customer)
        {
            if (customer == null)
                throw new Exception(string.Format("{0} cannot be null", nameof(customer)));
            if (Customers.ContainsKey(customer.Id))
            {
                Customers[customer.Id] = customer.Events;
            }
            else
            {
                Customers.Add(customer.Id, customer.Events);
            }
        }
    }
}
