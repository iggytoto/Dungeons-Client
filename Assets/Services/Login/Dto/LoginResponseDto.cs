using System;
using Services.Dto;

namespace Services.Login
{
    [Serializable]
    public class LoginResponseDto : ResponseBaseDto
    {
        public long userId;
        public string token;
    }
}