using System;

namespace Services.Dto
{
    [Serializable]
    public class TokenDto
    {
        public string value;
        public long validTo;
        public long userId;
    }
}