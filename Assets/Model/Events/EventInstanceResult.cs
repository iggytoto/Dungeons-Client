using System.Collections.Generic;

namespace Model.Events
{
    public class EventInstanceResult
    {
        public long EventInstanceId;
        public EventType EventType;
        public Dictionary<long, long> UnitsHitPoints;
    }
}