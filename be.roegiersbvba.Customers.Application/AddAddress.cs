using be.roegiersbvba.Customers.Commands;
using be.roegiersbvba.Customers.Dto;
using System;
using System.Linq;

namespace be.roegiersbvba.Customers.Application
{
    public class AddAddress : IHandle<CreateAddressForcustomer, IDto>
    {
        public void Handle(CreateAddressForcustomer address)
        {
            var customer = Domain.Repositories.Repository.Customers.Where(c => c.Id.Equals(address.CustomerId)).FirstOrDefault();
            customer.AddAddress(address.Street, address.City, address.ZipCode, address.Number, address.Function);
            customer.Events.ForEach(e => Console.WriteLine(e.GetType().ToString()));


            var nc = Domain.Customer.Replay(customer.Events);
        }

        public IDto HandleAndReturn(CreateAddressForcustomer address)
        {
            throw new NotImplementedException("no returntype for addresses;");
        }
    }
}
