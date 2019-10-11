using be.roegiersbvba.Customers.Commands;
using be.roegiersbvba.Customers.Domain.Repositories;
using be.roegiersbvba.Customers.Dto;
using System;

namespace be.roegiersbvba.Customers.Application
{
    public class ModifyFormOfPayment : IHandle<Commands.ModifyFormOfPayment, Dto.Customer>
    {
        public void Handle(Commands.ModifyFormOfPayment formOfPayment)
        {
            Process(formOfPayment);
        }

        public Customer HandleAndReturn(Commands.ModifyFormOfPayment command)
        {
            throw new NotImplementedException();
        }

        private Domain.Customer Process(Commands.ModifyFormOfPayment formOfPayment)
        {
            var customerEvents = CustomerRepository.GetCustomers(formOfPayment.CustomerId);
            var customer = (Domain.Customer)Domain.AggregateRoot.Replay(customerEvents); //fix.
            customer.AddCreditCard(formOfPayment.CardHolder, formOfPayment.CardNumber, formOfPayment.ExpirationMonth, formOfPayment.ExpirationYear);
            CustomerRepository.SaveCustomer(customer);
            return customer;
        }
    }
}
