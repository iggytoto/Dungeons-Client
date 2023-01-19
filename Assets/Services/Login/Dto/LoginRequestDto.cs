using System;
using Services.Common.Dto;

namespace Services.Login
{
    [Serializable]
    public class LoginRequestDto : RequestDto
    {
        public string login;
        public string password;
    }
}