

namespace be.roegiersbvba.Customers.Domain.Events
{
    internal class CreditCardInformationObjectCreated : EventBase<CreditCardInformation>, ICreatedValueObject, IEvent
    {
        public CreditCardInformationObjectCreated(string cardHolder, string cardNumber, string expirationMonth, string expirationYear)
        {
            CardHolder = cardHolder;
            CardNumber = cardNumber;
            ExpirationMonth = expirationMonth;
            ExpirationYear = expirationYear;
        }

        public string CardHolder { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpirationMonth { get; private set; }
        public string ExpirationYear { get; private set; }
    }
}
