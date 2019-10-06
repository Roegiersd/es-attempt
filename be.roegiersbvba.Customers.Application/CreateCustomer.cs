using System;

namespace be.roegiersbvba.Customers.Application
{
    public class CreateCustomer : IHandle<Commands.CreateCustomer, Dto.Customer>
    {
        public void Handle(Commands.CreateCustomer customer)
        {
            Process(customer);
        }

        public Dto.Customer HandleAndReturn(Commands.CreateCustomer customer)
        {
            return new Dto.Customer { Id = Process(customer).Id };
        }

        private Domain.Customer Process(Commands.CreateCustomer customer)
        {
            var customerEntity = new Domain.Customer(customer.Lastname, customer.Firstname, customer.CustomerType);
            Domain.Repositories.CustomerRepository.SaveCustomer(customerEntity);
            return customerEntity;
        }
    }
}
