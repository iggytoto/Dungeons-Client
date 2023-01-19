using System;
using System.Collections.Generic;
using Model.Events;
using Services.Common.Dto;

namespace Services.Events.Dto
{
    [Serializable]
    public class EventInstanceResultDto : RequestDto
    {
        public long eventInstanceId;
        public EventType eventType;
        public Dictionary<long, int> unitsHitPoints;

        public static EventInstanceResultDto FromDomain(EventInstanceResult r)
        {
            return new EventInstanceResultDto
            {
                eventInstanceId = r.EventInstanceId,
                eventType = r.EventType,
                unitsHitPoints = r.UnitsHitPoints
            };
        }
    }
}