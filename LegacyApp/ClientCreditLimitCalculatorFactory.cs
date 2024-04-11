namespace LegacyApp
{
    class ClientCreditLimitCalculatorFactory
    {
        public IUserCreditLimitAccessor CreditLimitAccessor { get; set; }

        public IClientCreditLimitCalculator CreateClientCreditLimitCalculator(string clientType)
        {
            if (clientType == "VeryImportantClient")
                return new VeryImportantClientCreditLimitCalculator();
            else if (clientType == "ImportantClient")
                return new ImportantClientCreditLimitCalculator{CreditLimitAccessor = CreditLimitAccessor};
            return new DefaultClientCreditLimitCalculator{CreditLimitAccessor = CreditLimitAccessor};
        }
    }
}
