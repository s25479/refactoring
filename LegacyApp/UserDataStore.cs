namespace LegacyApp
{
    class UserDataStore : IUserDataStore
    {
        public bool AddUser(User user)
        {
            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
