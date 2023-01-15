using System;
using Model.Events;
using Services.Common.Dto.Items;
using Services.Dto;

namespace Services.Events.Dto
{
    [Serializable]
    public class EventDto : ResponseBaseDto
    {
        public long id;
        public EventType eventType;
        public EventStatus status;

        public Event toDomain()
        {
            return new Event
            {
                Id = id,
                EventType = eventType,
                Status = status
            };
        }
    }
}