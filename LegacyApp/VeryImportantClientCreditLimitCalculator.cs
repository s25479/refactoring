namespace LegacyApp
{
    class VeryImportantClientCreditLimitCalculator : IClientCreditLimitCalculator
    {
        public UserCreditLimit GetCreditLimit()
        {
            return new UserCreditLimit{Present = false};
        }
    }
}
