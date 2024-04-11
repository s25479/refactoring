namespace LegacyApp
{
    class ImportantClientCreditLimitCalculator : IClientCreditLimitCalculator
    {
        public IUserCreditLimitAccessor CreditLimitAccessor { get; set; }

        public UserCreditLimit GetCreditLimit()
        {
            return new UserCreditLimit{CreditLimit = CreditLimitAccessor.AccessCreditLimit() * 2};
        }
    }
}
