using System;
using Model.Events;

namespace Services.Events
{
    [Serializable]
    public class EventInfo
    {
        public string Port { get; private set; }
        public string Host { get; private set; }
        public long EventId { get; private set; }
        public long EventInstanceId { get; private set; }
        public EventType EventType { get; private set; }

        public EventInfo(long eventInstanceId, EventType type, long eventId, string host = null, string port = null)
        {
            EventInstanceId = eventInstanceId;
            EventType = type;
            EventId = eventId;
            Host = host;
            Port = port;
        }
    }
}