using System;
using Services.Dto;

namespace Services.Login
{
    [Serializable]
    public class LoginResponse : ResponseBase
    {
        public long userId;
        public string token;
    }
}