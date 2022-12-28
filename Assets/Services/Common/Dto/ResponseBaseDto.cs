using System;

namespace Services.Dto
{
    [Serializable]
    public class ResponseBaseDto
    {
        public long code;
        public string message;
    }
}