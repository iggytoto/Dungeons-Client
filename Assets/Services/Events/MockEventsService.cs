using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Events;
using Model.Units;
using Model.Units.Humans;
using UnityEngine;
using EventType = Model.Events.EventType;

namespace Services.Events
{
    public class MockEventsService : MonoBehaviour, IEventsService
    {
        private static EventInfo _eventInfo;
        private static ObservableCollection<EventInfo> _eventInfos = new();

        private IBarrackService _barrackService;
        public string EndpointHttpType { get; set; }
        public string EndpointAddress { get; set; }
        public ushort EndpointPort { get; set; }

        public void InitService()
        {
        }

        public EventInfo EventInfo => _eventInfo;

        public ObservableCollection<EventInfo> EventInfos => _eventInfos;

        private void Start()
        {
            _barrackService = FindObjectOfType<GameService>().BarrackService;
        }

        public void Register(List<long> unitsIds, EventType type, Action<string> onError)
        {
            foreach (var unit in unitsIds.Select(unitId => _barrackService.AvailableUnits.First(u => u.Id == unitId)))
            {
                unit.activity = UnitActivity.Event;
            }

            EventInfos.Add(new EventInfo(1, type, 1, "127.0.0.1", "7777"));
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
            _eventInfo = new EventInfo(1, EventType.PhoenixRaid, 1);
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