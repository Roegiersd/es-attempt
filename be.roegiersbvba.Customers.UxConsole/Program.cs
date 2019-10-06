using System;
using be.roegiersbvba.Customers.Commands;
using be.roegiersbvba.Customers.Queries;

namespace be.roegiersbvba.Customers.UxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            var id = p.CreateCustomer();
            p.GetCustomer(id);
            p.AddAddress(id);
            p.GetCustomer(id);
            p.AddAddress2(id);
            p.GetCustomer(id);
            Console.WriteLine("done");
            Console.ReadLine();
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
    }
}
