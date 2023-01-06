using System;

namespace Services
{
    /**
     * Login service provides connection context for the client.
     */
    public interface ILoginService : IService
    {
        /***
        * Current connection state with login service. 
        */
        public ConnectionState ConnectionState { get; }

        /**
        * Current logged in user information
        */
        public UserContext UserContext { get; }

        /**
        * Logins with given credentials, if succeeded navigates to the town scene
        */
        public void TryLogin(string login, string password, EventHandler<UserContext> onSuccess);

        /**
        * Register user with given credentials.
        */
        public void Register(string login, string password);
    }
}