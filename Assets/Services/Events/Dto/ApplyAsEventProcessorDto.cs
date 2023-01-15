using System;

namespace Services.Events.Dto
{
    [Serializable]
    public class ApplyAsEventProcessorDto
    {
        public string host;
        public string port;

    }
}