using System;
using System.Collections.Generic;
using Model.Events;
using Services.Common.Dto;

namespace Services.Events.Dto
{
    [Serializable]
    public class EventRegisterRequestDto : RequestDto
    {
        public List<long> unitsIds;
        public EventType eventType;
    }
}