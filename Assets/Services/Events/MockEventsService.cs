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
using Random = System.Random;

namespace Services.Events
{
    public class MockEventsService : MonoBehaviour, IEventsService
    {
        private static readonly ObservableCollection<EventInstance> EventInstancesInfos = new();
        private static readonly Random Rng = new();

        private IBarrackService _barrackService;
        public string EndpointHttpType { get; set; }
        public string EndpointAddress { get; set; }
        public ushort EndpointPort { get; set; }

        public void InitService()
        {
        }

        public ObservableCollection<EventInstance> EventInstances => EventInstancesInfos;

        public void Register(List<long> unitsIds, EventType type, Action<Event> onSuccess, Action<string> onError)
        {
            foreach (var unit in unitsIds.Select(unitId => _barrackService.AvailableUnits.First(u => u.Id == unitId)))
            {
                unit.activity = UnitActivity.Event;
            }

            var e = new Event { Id = 1, Status = EventStatus.Planned, EventType = type };
            EventInstances.Add(GetDefaultEventInstance(type));
            onSuccess?.Invoke(e);
        }

        public void Cancel(long eventId, Action<string> onError)
        {
            EventInstances.Remove(EventInstances.First(ei => ei.eventId == eventId));
        }

        public void Status(Action<List<EventInstance>> onSuccess, Action<string> onError)
        {
            onSuccess?.Invoke(EventInstances.ToList());
        }

        public void ApplyAsServer(string host, string port, Action<EventInstance> onSuccessHandler,
            Action<string> onError)
        {
            var ei = GetDefaultEventInstance(EventType.PhoenixRaid);
            EventInstances.Add(ei);
            onSuccessHandler?.Invoke(ei);
        }

        public void GetEventInstanceRosters(long eventInstanceId, Action<List<Unit>> onSuccessHandler,
            Action<string> onError)
        {
            onSuccessHandler?.Invoke(
                new List<Unit>
                {
                    new HumanWarrior { ownerId = 1, Id = 1 },
                    new HumanWarrior { ownerId = 2, Id = 2 },
                });
        }

        public void SaveResult(EventInstanceResult result, Action<string> onError)
        {
            EventInstances.Remove(EventInstances.First(ei => ei.id == result.EventInstanceId));
        }

        private void Start()
        {
            _barrackService = FindObjectOfType<GameService>().BarrackService;
        }

        private static EventInstance GetDefaultEventInstance(EventType type)
        {
            return new EventInstance
            {
                host = "localhost",
                port = "7777",
                id = 1,
                status = EventInstanceStatus.WaitingForPlayers,
                eventId = 1,
                eventType = type
            };
        }
    }
}