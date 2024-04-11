namespace LegacyApp
{
    public class DefaultClientCreditLimitCalculator
    {
        public UserCreditLimitAccessor CreditLimitAccessor { get; set; }

        public UserCreditLimit GetCreditLimit()
        {
            return new UserCreditLimit{Present = true, CreditLimit = CreditLimitAccessor.AccessCreditLimit()};
        }
    }
}
