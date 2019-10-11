using System;
using System.Dynamic;
using be.roegiersbvba.Customers.Domain.Events;

namespace be.roegiersbvba.Customers.Domain
{
    internal class CreditCardAdded : EventBase<Customer>, IEvent, ICreditCardInformationCreated
    {
        public CreditCardAdded(Guid customerId, ICreditCardInformationCreated creditCardInformation) : base()
        {
            Id = customerId;
            _cardHolder = creditCardInformation.CardHolder;
            _cardNumber = creditCardInformation.CardNumber;
            _expirationMonth = creditCardInformation.ExpirationMonth;
            _expirationYear = creditCardInformation.ExpirationYear;
        }


        private readonly string _cardHolder;
        public string CardHolder
        {
            get { return _cardHolder; }
        }

        private readonly string _cardNumber;
        public string CardNumber
        {
            get { return _cardNumber; }
        }


        private readonly string _expirationMonth;
        public string ExpirationMonth
        {
            get { return _expirationMonth; }
        }


        private readonly string _expirationYear;
        public string ExpirationYear
        {
            get { return _expirationYear; }
        }
    }
}