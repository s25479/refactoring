namespace LegacyApp
{
    class DefaultClientCreditLimitCalculator : IClientCreditLimitCalculator
    {
        public IUserCreditLimitAccessor CreditLimitAccessor { get; set; }

        public UserCreditLimit GetCreditLimit()
        {
            return new UserCreditLimit{Present = true, CreditLimit = CreditLimitAccessor.AccessCreditLimit()};
        }
    }
}
