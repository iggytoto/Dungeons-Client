using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Events;
using Services.Common.Dto;
using Services.Dto;
using Services.Events.Dto;
using Unity.VisualScripting;
using UnityEngine;
using Event = Model.Events.Event;
using EventType = Model.Events.EventType;

namespace Services.Events
{
    public class EventsService : ServiceBase, IEventsService
    {
        private static EventInstance _eventInfo;
        private static ObservableCollection<EventInstance> _eventInfos = new();

        [SerializeField] private float eventsStatusRefreshInterval = 60;

        private const string RegisterPath = "/events/register";
        private const string StatusPath = "/events/status";
        private const string ApplyAsServerPath = "/events/applyAsServer";
        private const string SaveResultPath = "/events/saveEventInstanceResult";
        private const string GetDataPath = "/events/getData";


        private float _currentEventStatusRefreshTimer;
        public EventInstance EventInfo => _eventInfo;
        public ObservableCollection<EventInstance> EventInfos => _eventInfos;


        private void Update()
        {
            _currentEventStatusRefreshTimer -= Time.deltaTime;
            if (!(_currentEventStatusRefreshTimer <= 0)) return;
            RefreshEventsStatuses(OnRefreshEventsSuccess, null);
            _currentEventStatusRefreshTimer = eventsStatusRefreshInterval;
        }

        public void Register(List<long> unitsIds, EventType type, Action<Event> onSuccess, Action<string> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<EventDto>(
                    RegisterPath,
                    new EventRegisterRequestDto
                    {
                        eventType = type,
                        unitsIds = unitsIds
                    },
                    ApiAdapter.Post,
                    dt => onSuccess?.Invoke(dt.toDomain()),
                    dto => onError?.Invoke(dto.message)));
        }

        public void Cancel(long eventId, Action<string> onError)
        {
            throw new NotImplementedException();
        }

        public void Status(Action<List<EventInstance>> onSuccess, Action<string> onError)
        {
            RefreshEventsStatuses(statuses => { onSuccess?.Invoke(statuses); }, onError);
            _currentEventStatusRefreshTimer = eventsStatusRefreshInterval;
        }

        private void RefreshEventsStatuses(Action<List<EventInstance>> onSuccess, Action<string> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ListResponseDto<EventInstanceDto>>(
                    StatusPath,
                    null,
                    ApiAdapter.Get,
                    dto =>
                    {
                        var eventStatuses = dto.items.Select(d => d.ToDomain()).ToList();
                        onSuccess?.Invoke(eventStatuses);
                        OnRefreshEventsSuccess(eventStatuses);
                    },
                    dto => onError?.Invoke(dto.message)));
        }

        public void ApplyAsServer(
            string host,
            string port,
            Action<EventInstance> onSuccessHandler,
            Action<string> onError)
        {
            if (EventInfo != null)
            {
                throw new InvalidOperationException(
                    "Server should process current event instance before register to the another one");
            }

            StartCoroutine(
                APIAdapter.DoRequestCoroutine<EventInstanceDto>(
                    ApplyAsServerPath,
                    new ApplyAsEventProcessorDto { host = host, port = port },
                    ApiAdapter.Post,
                    dto => OnSuccessApplyAsServer(onSuccessHandler, dto),
                    dto => onError?.Invoke(dto.message)));
        }

        private void OnSuccessApplyAsServer(Action<EventInstance> onSuccessHandler, EventInstanceDto dto)
        {
            _eventInfo = dto?.ToDomain();
            onSuccessHandler?.Invoke(dto?.ToDomain());
        }

        public void GetEventInstanceRosters(Action<List<Unit>> onSuccessHandler,
            Action<string> onError)
        {
            if (EventInfo == null)
            {
                Debug.LogWarning("Cannot request event instance data, event info in not being received yet");
                return;
            }

            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ListResponseDto<UnitDto>>(
                    GetDataPath,
                    new GetEventInstanceDataRequestDto
                        { eventInstanceId = EventInfo.id },
                    ApiAdapter.Post,
                    dto => onSuccessHandler?.Invoke(dto.items.Select(d => d.ToDomain()).ToList()),
                    dto => onError?.Invoke(dto.message)));
        }

        public void SaveResult(EventInstanceResult result, Action<string> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                    SaveResultPath,
                    EventInstanceResultDto.FromDomain(result),
                    ApiAdapter.Post,
                    OnSuccessSaveResult,
                    dto => onError?.Invoke(dto.message)));
        }

        private void OnSuccessSaveResult(ResponseBaseDto e)
        {
            _eventInfo = null;
        }


        private void OnRefreshEventsSuccess(List<EventInstance> instances)
        {
            if (instances == null) return;
            EventInfos.AddRange(instances.Where(eventDto => EventInfos.All(ei => ei.id != eventDto.id)));
        }
    }
}