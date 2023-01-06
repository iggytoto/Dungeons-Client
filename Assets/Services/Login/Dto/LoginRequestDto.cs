using System;

namespace Services.Login
{
    [Serializable]
    public class LoginRequestDto
    {
        public string login;
        public string password;
    }
}