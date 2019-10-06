using be.roegiersbvba.Customers.Commands;
using be.roegiersbvba.Customers.Dto;
using System;
using be.roegiersbvba.Customers.Domain.Repositories;

namespace be.roegiersbvba.Customers.Application
{
    public class AddAddress : IHandle<CreateAddressForcustomer, IDto>
    {
        public void Handle(CreateAddressForcustomer address)
        {
            var customerEvents = CustomerRepository.GetCustomers(address.CustomerId);
            var customer = (Domain.Customer)Domain.AggregateRoot.Replay(customerEvents); //fix.
            customer.AddAddress(address.Street, address.City, address.ZipCode, address.Number, address.Function);
            CustomerRepository.SaveCustomer(customer);
        }

        public IDto HandleAndReturn(CreateAddressForcustomer address)
        {
            throw new NotImplementedException("No returntype defined in method.");
        }


    }
}
