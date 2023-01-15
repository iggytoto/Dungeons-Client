using System;
using System.Collections.Generic;
using Model.Events;

namespace Services.Events.Dto
{
    [Serializable]
    public class EventInstanceResultDto
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