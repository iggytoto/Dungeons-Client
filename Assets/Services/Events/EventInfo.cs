using System;
using Model.Events;

namespace Services.Events
{
    [Serializable]
    public class EventInfo
    {
        public long EventInstanceId { get; private set; }
        public EventType EventType { get; private set; }

        public EventInfo(long eventInstanceId, EventType type)
        {
            EventInstanceId = eventInstanceId;
            EventType = type;
        }
    }
}