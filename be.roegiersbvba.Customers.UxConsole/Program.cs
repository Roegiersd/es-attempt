using System;
using be.roegiersbvba.Customers.Commands;

namespace be.roegiersbvba.Customers.UxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Dto.Customer customer = new Application.CreateCustomer().HandleAndReturn((new Commands.CreateCustomer("John", "Doe", "enterprise")));
            Console.WriteLine(string.Format("Customer with id {0} created", customer.Id.ToString() ?? "unknown"));

            new Application.AddAddress().Handle(new CreateAddressForcustomer("Bellevue", "Gent", "9000", "1", "Headquarters", customer.Id));
            Console.ReadLine();
        }
    }
}
