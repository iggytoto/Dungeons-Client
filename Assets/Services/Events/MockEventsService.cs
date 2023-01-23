using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Events;
using Model.Units;
using Model.Units.Humans;
using UnityEngine;
using Event = Model.Events.Event;
using EventType = Model.Events.EventType;

namespace Services.Events
{
    public class MockEventsService : MonoBehaviour, IEventsService
    {
        private static EventInstance _eventInfo;
        private static readonly ObservableCollection<EventInstance> _eventInfos = new();

        private IBarrackService _barrackService;
        public string EndpointHttpType { get; set; }
        public string EndpointAddress { get; set; }
        public ushort EndpointPort { get; set; }

        public void InitService()
        {
        }

        public EventInstance EventInfo => _eventInfo;

        public ObservableCollection<EventInstance> EventInfos => _eventInfos;

        private void Start()
        {
            _barrackService = FindObjectOfType<GameService>().BarrackService;
        }

        public void Register(List<long> unitsIds, EventType type, Action<Event> onSuccess, Action<string> onError)
        {
            foreach (var unit in unitsIds.Select(unitId => _barrackService.AvailableUnits.First(u => u.Id == unitId)))
            {
                unit.activity = UnitActivity.Event;
            }

            onSuccess?.Invoke(new Event() { Id = 1, Status = EventStatus.Planned, EventType = type });
        }

        public void Cancel(long eventId, Action<string> onError)
        {
            EventInfos.Clear();
        }

        public void Status(Action<List<EventInstance>> onSuccess, Action<string> onError)
        {
            onSuccess?.Invoke(EventInfos.ToList());
        }

        public void ApplyAsServer(string host, string port, Action<EventInstance> onSuccessHandler,
            Action<string> onError)
        {
            if (EventInfo != null)
                throw new InvalidOperationException();
            onSuccessHandler?.Invoke(
                new EventInstance
                {
                    host = "localhost",
                    port = "7777",
                    id = 1,
                    status = EventInstanceStatus.WaitingForPlayers,
                    eventId = 1,
                    eventType = EventType.PhoenixRaid
                });
            _eventInfo = new EventInstance { eventType = EventType.PhoenixRaid };
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
            _eventInfo = null;
        }
    }
}