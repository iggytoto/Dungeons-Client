using System;
using Services.Common.Dto;

namespace Services.Events.Dto
{
    [Serializable]
    public class ApplyAsEventProcessorDto : RequestDto
    {
        public string host;
        public string port;
    }
}