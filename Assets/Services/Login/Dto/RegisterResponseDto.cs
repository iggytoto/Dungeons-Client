using System;
using Services.Dto;

namespace Services.Login
{
    [Serializable]
    public class RegisterResponseDto : ResponseBaseDto
    {
        public long userId;
    }
}