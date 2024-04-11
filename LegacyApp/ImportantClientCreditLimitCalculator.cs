namespace LegacyApp
{
    public class ImportantClientCreditLimitCalculator
    {
        public UserCreditLimitAccessor CreditLimitAccessor { get; set; }

        public UserCreditLimit GetCreditLimit()
        {
            return new UserCreditLimit{CreditLimit = CreditLimitAccessor.AccessCreditLimit() * 2};
        }
    }
}
