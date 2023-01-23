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
        public Dictionary<long, long> UnitsHitPoints;

        public static EventInstanceResultDto FromDomain(EventInstanceResult r)
        {
            return new EventInstanceResultDto
            {
                eventInstanceId = r.EventInstanceId,
                eventType = r.EventType,
                UnitsHitPoints = r.UnitsHitPoints
            };
        }
    }
}