using System;


namespace be.roegiersbvba.Customers.Commands
{
    public class CreateAddressForcustomer : ICommand
    {
        public CreateAddressForcustomer(string street, string city, string zipCode, string number, string function, Guid customerId)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Function = function ?? throw new ArgumentNullException(nameof(function));
            CustomerId = customerId;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Number { get; private set; }
        public string Function { get; private set; }
        public Guid CustomerId { get; private set; }
    }

    public class ChangeAddressFunctionForcustomer : ICommand
    {
        public ChangeAddressFunctionForcustomer(string function, Guid customerId, Guid addressId)
        {
            Function = function;
            AddressId = addressId;
        }

        public string Function { get; private set; }
        public Guid AddressId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
