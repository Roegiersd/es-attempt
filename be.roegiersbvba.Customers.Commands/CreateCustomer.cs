using System;

namespace be.roegiersbvba.Customers.Commands
{
    public class CreateCustomer : ICommand
    {
        public CreateCustomer(string firstName, string lastName, string customerType)
        {
            Firstname = firstName;
            Lastname = lastName;
            CustomerType = customerType;
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CustomerType { get; set; }
    }
}
