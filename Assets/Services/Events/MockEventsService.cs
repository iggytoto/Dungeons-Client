using System;
using System.Collections.Generic;
using System.Linq;
using Model.Events;
using Model.Units;
using Model.Units.Humans;
using Services.Dto;
using UnityEngine;
using Event = Model.Events.Event;
using EventType = Model.Events.EventType;

namespace Services.Events
{
    public class MockEventsService : MonoBehaviour, IEventsService
    {
        private IBarrackService _barrackService;
        public string EndpointHttpType { get; set; }
        public string EndpointAddress { get; set; }
        public ushort EndpointPort { get; set; }

        private void Start()
        {
            _barrackService = FindObjectOfType<GameService>().BarrackService;
        }

        public void InitService()
        {
        }

        public EventInfo EventInfo { get; private set; }

        public void Register(List<long> unitsIds, EventType type, Action<string> onError)
        {
            foreach (var unitId in unitsIds)
            {
                var unit = _barrackService.AvailableUnits.First(u => u.Id == unitId);
                unit.activity = UnitActivity.Event;
            }
        }

        public void Status(Action<List<Event>> onSuccessHandler, Action<string> onError)
        {
            throw new NotImplementedException();
        }

        public void ApplyAsServer(string host, string port, Action<EventInstance> onSuccessHandler,
            Action<string> onError)
        {
            if (EventInfo != null)
                throw new InvalidOperationException();
            onSuccessHandler?.Invoke(
                new EventInstance
                {
                    Host = "localhost",
                    Port = "7777",
                    Id = 1,
                    Status = EventInstanceStatus.WaitingForPlayers,
                    EventId = 1,
                    EventType = EventType.PhoenixRaid
                });
            EventInfo = new EventInfo(1, EventType.PhoenixRaid);
        }

        public void GetEventInstanceRosters(Action<List<Unit>> onSuccessHandler,
            Action<string> onError)
        {
            if (EventInfo == null)
                throw new InvalidOperationException();
            onSuccessHandler?.Invoke(
                new List<Unit>
                {
                    new HumanWarrior { ownerId = 1, Id = 1 },
                    new HumanWarrior { ownerId = 2, Id = 2 },
                });
        }

        public void SaveResult(EventInstanceResult result, Action<string> onError)
        {
            if (EventInfo == null)
                throw new InvalidOperationException();
            EventInfo = null;
        }
    }
}