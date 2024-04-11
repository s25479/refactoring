namespace LegacyApp
{
    public class UserCreditLimitCalculator
    {
        public UserCreditLimitAccessor CreditLimitAccessor { get; set; }

        public UserCreditLimit CalculateUserCreditLimit(string clientType)
        {
            if (clientType == "VeryImportantClient")
                return new VeryImportantClientCreditLimitCalculator().GetCreditLimit();
            else if (clientType == "ImportantClient")
                return new ImportantClientCreditLimitCalculator{CreditLimitAccessor = CreditLimitAccessor}.GetCreditLimit();
            return new DefaultClientCreditLimitCalculator{CreditLimitAccessor = CreditLimitAccessor}.GetCreditLimit();
        }
    }
}
