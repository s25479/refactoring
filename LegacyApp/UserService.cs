using System;

namespace LegacyApp
{
    public class UserService
    {
        private IClientAccessor clientAccessor;
        private ITimeProvider timeProvider;
        private IUserDataStore userDataStore;

        internal UserService(IClientAccessor clientAccessor, ITimeProvider timeProvider, IUserDataStore userDataStore)
        {
            this.clientAccessor = clientAccessor;
            this.timeProvider = timeProvider;
            this.userDataStore = userDataStore;
        }

        public UserService()
        {
            this.clientAccessor = new DefaultClientAccessor();
            this.timeProvider = new TimeProvider();
            this.userDataStore = new UserDataStore();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var userCreationParameters = new UserCreationParameters
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                CurrentTime = timeProvider.Now
            };
            return userCreationParameters.Valid() && CreateUserForClient(clientAccessor.GetClientById(clientId), userCreationParameters);
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
            return AssignCreditLimitToUser(user, client.Type) && userDataStore.AddUser(user);
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
            return new ClientCreditLimitCalculatorFactory
            {
                CreditLimitAccessor = new UserCreditLimitAccessor
                {
                    UserLastName = user.LastName,
                    UserDateOfBirth = user.DateOfBirth
                }
            };
        }
    }
}
