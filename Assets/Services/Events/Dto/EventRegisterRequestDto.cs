using System;
using System.Collections.Generic;
using Model.Events;

namespace Services.Events.Dto
{
    [Serializable]
    public class EventRegisterRequestDto
    {
        public List<long> unitsIds;
        public EventType eventType;
    }
}