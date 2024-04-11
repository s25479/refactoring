namespace LegacyApp
{
    class UserCreditLimit
    {
        public bool Present { get; set; }
        public int CreditLimit { get; set; }

        public bool Valid()
        {
            return !Present || CreditLimit >= 500;
        }
    }
}
