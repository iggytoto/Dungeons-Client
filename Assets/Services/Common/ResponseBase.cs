using System;

namespace Services.Dto
{
    [Serializable]
    public abstract class ResponseBase
    {
        public long code;
        public string message;
    }
}