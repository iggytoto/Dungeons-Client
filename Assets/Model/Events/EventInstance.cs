using System;

namespace Model.Events
{
    [Serializable]
    public class EventInstance
    {
        public long id;
        public long eventId;
        public string host;
        public string port;
        public EventInstanceStatus status;
        public EventType eventType;
    }
}