using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var userCreationParameters = new UserCreationParameters
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                CurrentTime = DateTime.Now
            };
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
            return AssignCreditLimitToUser(user, client.Type) && StoreUser(user);
        }

        private static bool AssignCreditLimitToUser(User user, string clientType)
        {
            UserCreditLimit creditLimit = GetCreditLimitForUser(user, clientType);
            if (!creditLimit.Valid())
                return false;

            user.HasCreditLimit = creditLimit.Present;
            user.CreditLimit = creditLimit.CreditLimit;
            return true;
        }

        private static bool StoreUser(User user)
        {
            UserDataAccess.AddUser(user);
            return true;
        }

        private static UserCreditLimit GetCreditLimitForUser(User user, string clientType)
        {
            return GetClientCreditLimitCalculator(user, clientType).GetCreditLimit();
        }

        private static IClientCreditLimitCalculator GetClientCreditLimitCalculator(User user, string clientType)
        {
            return GetClientCreditLimitCalculatorFactory(user).CreateClientCreditLimitCalculator(clientType);
        }

        private static ClientCreditLimitCalculatorFactory GetClientCreditLimitCalculatorFactory(User user)
        {
            return new ClientCreditLimitCalculatorFactory{
                CreditLimitAccessor = new UserCreditLimitAccessor
                {
                    UserLastName = user.LastName,
                    UserDateOfBirth = user.DateOfBirth
                }
            };
        }
    }
}
