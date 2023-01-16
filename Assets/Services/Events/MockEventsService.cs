using System;
using System.Collections.Generic;
using Model.Events;
using Model.Units.Humans;
using Services.Dto;

namespace Services.Events
{
    public class MockEventsService : IEventsService
    {
        public string EndpointHttpType { get; set; }
        public string EndpointAddress { get; set; }
        public ushort EndpointPort { get; set; }

        public void InitService()
        {
            throw new NotImplementedException();
        }

        public void Register(List<long> unitsIds, EventType type, EventHandler<ErrorResponseDto> onError)
        {
            throw new NotImplementedException();
        }

        public void Status(EventHandler<List<Event>> onSuccessHandler, EventHandler<ErrorResponseDto> onError)
        {
            throw new NotImplementedException();
        }

        public void ApplyAsServer(string host, string port, EventHandler<EventInstance> onSuccessHandler,
            EventHandler<ErrorResponseDto> onError)
        {
            onSuccessHandler?.Invoke(this,
                new EventInstance
                {
                    Host = "localhost",
                    Port = "7777",
                    Id = 1,
                    Status = EventInstanceStatus.WaitingForPlayers,
                    EventId = 1,
                    EventType = EventType.PhoenixRaid
                });
        }

        public void GetEventInstanceRosters(long eventInstanceId, EventHandler<List<Unit>> onSuccessHandler,
            EventHandler<ErrorResponseDto> onError)
        {
            onSuccessHandler?.Invoke(this,
                new List<Unit>
                {
                    new HumanWarrior { ownerId = 1, Id = 1 },
                    new HumanWarrior { ownerId = 2, Id = 2 },
                });
        }

        public void SaveResult(EventInstanceResult result, EventHandler<ErrorResponseDto> onError)
        {
            throw new NotImplementedException();
        }
    }
}