using System;
using Services.Dto;

namespace Services.Login
{
    [Serializable]
    public class RegisterResponse : ResponseBaseDto
    {
        public long userId;
    }
}