using System;
using Services.Dto;

namespace Services.Login
{
    [Serializable]
    public class RegisterResponse : ResponseBase
    {
        public long userId;
    }
}