using System;
using System.Collections.Generic;
using System.Text;

namespace be.roegiersbvba.Customers.Commands
{
    public class ModifyFormOfPayment : ICommand
    {

        public ModifyFormOfPayment(string cardHolder, string cardNumber, string expirationMonth, string expirationYear, Guid customerId)
        {
            CardHolder = cardHolder ?? throw new ArgumentNullException(nameof(cardHolder));
            CardNumber = cardNumber ?? throw new ArgumentNullException(nameof(cardNumber));
            ExpirationMonth = expirationMonth ?? throw new ArgumentNullException(nameof(expirationMonth));
            ExpirationYear = expirationYear ?? throw new ArgumentNullException(nameof(expirationMonth));
            CustomerId = customerId;
        }

        public string CardHolder { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpirationMonth { get; private set; }
        public string ExpirationYear { get; private set; }
        public Guid CustomerId { get; private set; }
    }
}
