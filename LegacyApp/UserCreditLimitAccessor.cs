using System;

namespace LegacyApp
{
    class UserCreditLimitAccessor : IUserCreditLimitAccessor
    {
        public string UserLastName { get; set; }
        public DateTime UserDateOfBirth { get; set; }

        public int AccessCreditLimit()
        {
            using (var userCreditService = new UserCreditService())
            {
                return userCreditService.GetCreditLimit(UserLastName, UserDateOfBirth);
            }
        }
    }
}
