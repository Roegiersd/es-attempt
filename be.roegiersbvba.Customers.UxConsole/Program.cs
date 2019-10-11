using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization.Json;
using be.roegiersbvba.Customers.Commands;
using be.roegiersbvba.Customers.Domain;
using be.roegiersbvba.Customers.Queries;

namespace be.roegiersbvba.Customers.UxConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var p = new Program();

            var id = p.CreateCustomer();
            p.GetCustomer(id);
            p.AddAddress(id);
            p.GetCustomer(id);
            p.AddAddress2(id);
            p.GetCustomer(id);
            p.AddCreditCardInformation(id);
            p.GetCustomer(id);
            p.GetDomainCustomer(id);
            Console.WriteLine("done");
            Console.ReadLine();

        }

        public void AddCreditCardInformation(Guid customerId)
        {
            new Application.ModifyFormOfPayment().Handle(new ModifyFormOfPayment("Davy Roegiers", "123-456", "04", "28", customerId));
            Console.WriteLine("Added form of payment");
        }

        public Guid CreateCustomer()
        {
            Dto.Customer customer = new Application.CreateCustomer().HandleAndReturn((new Commands.CreateCustomer("John", "Doe", "enterprise")));
            Console.WriteLine("Created customer");
            return customer.Id;
        }

        public void AddAddress(Guid customerId)
        {
            new Application.AddAddress().Handle(new CreateAddressForcustomer("Bellevue", "Gent", "9000", "1",
                "Headquarters", customerId));
            Console.WriteLine("Added address");
        }

        public void AddAddress2(Guid customerId)
        {
            new Application.AddAddress().Handle(new CreateAddressForcustomer("Winston Churchill Laan", "Gent", "9000",
                "1", "Stroom", customerId));
            Console.WriteLine("Added address 2");
        }

        public void GetCustomer(Guid id)
        {
            var customerEvents = new Application.GetCustomer().Execute(new GetCustomerById() { Id = id });
            foreach (var @event in customerEvents.Events)
            {
                Console.WriteLine(@event.ToString());
            }
        }

        public void GetDomainCustomer(Guid id)
        {
            var res = new Application.GetCustomer().Execute(id);
            Console.WriteLine(res.CreditCards.SingleOrDefault()?.CardHolder);
        }
    }
}
