using System;
using Services.Common.Dto;

namespace Services.Login
{
    [Serializable]
    public class RegisterRequestDto : RequestDto
    {
        public string login;
        public string password;
    }
}