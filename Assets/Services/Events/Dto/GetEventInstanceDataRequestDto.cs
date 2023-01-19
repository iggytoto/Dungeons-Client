using System;
using Services.Common.Dto;

namespace Services.Events.Dto
{
    [Serializable]
    public class GetEventInstanceDataRequestDto : RequestDto
    {
        public long eventInstanceId;
    }
}