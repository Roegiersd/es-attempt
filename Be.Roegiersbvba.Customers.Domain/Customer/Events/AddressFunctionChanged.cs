using System;

namespace be.roegiersbvba.Customers.Domain.Events
{
    public class AddressFunctionChanged : IEvent
    {
        public AddressFunctionChanged(string function)
        {
            Function = function;
        }
        public string Function { get; private set; }
        public Guid Id { get; private set; }
    }
}
