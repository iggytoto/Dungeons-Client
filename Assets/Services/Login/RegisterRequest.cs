using System;

namespace Services.Login
{
    [Serializable]
    public class RegisterRequest
    {
        public string login;
        public string password;
    }
}