using System.Security.Principal;

namespace be.roegiersbvba.Customers.Domain
{
    internal class AddressAdded : IEvent
    {
        public AddressAdded(string street, string city, string zip, string number, string function)
        {
            this.Street = street;
            this.City = city;
            this.Zip = zip;
            this.Number = number;
            this.Function = function;

        }

        public AddressAdded(Address address)
        {
            this.Street = address.Street;
            this.City = address.City;
            this.Zip = address.ZipCode;
            this.Number = address.Number;
            this.Function = address.Function;
            Address = address;
        }

        public Address Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Number { get; set; }
        public string Function { get; set; }
    }
}