namespace LegacyApp
{
    class DefaultClientAccessor : IClientAccessor
    {
        public Client GetClientById(int clientId)
        {
            return new ClientRepository().GetById(clientId);
        }
    }
}
