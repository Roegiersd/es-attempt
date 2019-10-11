using System;

namespace be.roegiersbvba.Customers.Domain
{
    public sealed class CreditCardInformation : ValueObjectBase<CreditCardInformation>, ICreditCardInformationCreated
    {
        public string CardHolder { get; private set; }
        public string CardNumber { get; private set; }
        public string ExpirationMonth { get; private set; }
        public string ExpirationYear { get; private set; }

        public static class CreditCardInformationFactory
        {
            public static ICreditCardInformationCreated CreateCreditCardInformation(string cardHolder, string cardNumber, string expirationMonth,
                string expirationYear)
            {
                var result = new CreditCardInformation(cardHolder, cardNumber, expirationMonth, expirationYear);
                return result;
            }

            public static CreditCardInformation ReplayCreditCardInformation(ICreditCardInformationCreated @event)
            {
                var result = new CreditCardInformation(@event);
                return result;
            }
        }

        private CreditCardInformation(string cardHolder, string cardNumber, string expirationMonth,
            string expirationYear)
        {
            if (!(string.IsNullOrEmpty(cardHolder) || cardHolder.Length > 20 || cardHolder.Length < 10))
            {
                CardHolder = cardHolder;
                CardNumber = cardNumber;
                ExpirationMonth = expirationMonth;
                ExpirationYear = expirationYear;
            }
            else
            {
                throw new DomainException("Cardholder is required and must be between 10 and 20 characters long");
            }
        }

        public CreditCardInformation(ICreditCardInformationCreated @event)
        {
            CardHolder = @event.CardHolder;
            CardNumber = @event.CardNumber;
            ExpirationMonth = @event.ExpirationMonth;
            ExpirationYear = @event.ExpirationYear;
        }
    }
    public interface ICreditCardInformationCreated : ICanBeValueObject
    {
        string CardHolder { get; }
        string CardNumber { get; }
        string ExpirationMonth { get; }
        string ExpirationYear { get; }
    }
}




