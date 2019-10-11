using System;
using be.roegiersbvba.Customers.Domain.Repositories;
using be.roegiersbvba.Customers.Dto;
using be.roegiersbvba.Customers.Queries;

namespace be.roegiersbvba.Customers.Application
{
    public class GetCustomer : IExecute<GetCustomerById, Dto.CustomerEvents>
    {
        public CustomerEvents Execute(GetCustomerById query)
        {
            var result = new CustomerEvents() { Events = CustomerRepository.GetCustomers(query.Id) };
            return result;
        }

        public Domain.Customer Execute(Guid id)
        {
            Domain.Customer result =  (Domain.Customer) Domain.Customer.Replay(CustomerRepository.GetCustomers(id));
            return result;
        }
    }
}