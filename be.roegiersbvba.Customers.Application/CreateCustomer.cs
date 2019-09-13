using System;

namespace be.roegiersbvba.Customers.Application
{
    public class CreateCustomer : IHandle<Commands.CreateCustomer, Dto.Customer>
    {
        public void Handle(Commands.CreateCustomer customer)
        {
            var x = new Domain.Customer(customer.Lastname, customer.Firstname, customer.CustomerType);
            Domain.Repositories.Repository.Customers.Add(x);
            //define edges return x?;
        }

        public Dto.Customer HandleAndReturn(Commands.CreateCustomer customer)
        {
            var x = new Domain.Customer(customer.Lastname, customer.Firstname, customer.CustomerType);
            var result = new Dto.Customer();
            result.Id = x.Id;
            Domain.Repositories.Repository.Customers.Add(x);
            return result;
        }
    }

}

