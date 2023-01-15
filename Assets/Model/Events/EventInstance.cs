namespace Model.Events
{
    public class EventInstance
    {
        public long Id;
        public long EventId;
        public string Host;
        public string Port;
        public EventInstanceStatus Status;
        public EventType EventType;
    }
}