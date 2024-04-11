using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var userCreationParameters = new UserCreationParameters{
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                CurrentTime = DateTime.Now};
            return userCreationParameters.Valid() && CreateUserForClient(new ClientRepository().GetById(clientId), userCreationParameters);
        }
      
        private bool CreateUserForClient(Client client, UserCreationParameters userCreationParameters)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = userCreationParameters.DateOfBirth,
                EmailAddress = userCreationParameters.EmailAddress,
                FirstName = userCreationParameters.FirstName,
                LastName = userCreationParameters.LastName
            };

            UserCreditLimit creditLimit = new UserCreditLimitCalculator{CreditLimitAccessor = new UserCreditLimitAccessor{
                UserLastName = user.LastName,
                UserDateOfBirth = user.DateOfBirth}}.CalculateUserCreditLimit(client.Type);
            if (!creditLimit.Valid())
                return false;

            user.HasCreditLimit = creditLimit.Present;
            user.CreditLimit = creditLimit.CreditLimit;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
