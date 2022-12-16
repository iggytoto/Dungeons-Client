using System;
using Services.Dto;

namespace Services.Login
{
    [Serializable]
    public class LoginResponse : ResponseBaseDto
    {
        public long userId;
        public string token;
    }
}