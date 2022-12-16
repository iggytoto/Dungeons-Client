using System;

namespace Services.Dto
{
    [Serializable]
    public abstract class ResponseBaseDto
    {
        public long code;
        public string message;
    }
}