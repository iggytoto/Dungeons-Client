using System;

namespace Services.Login
{
    [Serializable]
    public class LoginRequest
    {
        public string login;
        public string password;
    }
}