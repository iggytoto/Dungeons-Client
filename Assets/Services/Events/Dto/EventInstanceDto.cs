using System;
using Model.Events;
using Services.Dto;

namespace Services.Events.Dto
{
    [Serializable]
    public class EventInstanceDto : ResponseBaseDto
    {
        public long id;
        public long eventId;
        public string host;
        public string port;
        public EventInstanceStatus status;
        public EventType eventType;

        public EventInstance ToDomain()
        {
            return new EventInstance
            {
                id = id,
                host = host,
                port = port,
                status = status,
                eventId = eventId,
                eventType = eventType
            };
        }
    }
}