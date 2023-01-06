using System;

namespace Services.Login
{
    [Serializable]
    public class RegisterRequestDto
    {
        public string login;
        public string password;
    }
}