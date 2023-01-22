using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Events;
using Services.Common.Dto;
using Services.Dto;
using Services.Events.Dto;
using UnityEngine;
using EventType = Model.Events.EventType;

namespace Services.Events
{
    public class EventsService : ServiceBase, IEventsService
    {
        private static EventInfo _eventInfo;
        private static ObservableCollection<EventInfo> _eventInfos = new();

        [SerializeField] private float eventsStatusRefreshInterval = 15;

        private const string RegisterPath = "/events/register";
        private const string StatusPath = "/events/status";
        private const string ApplyAsServerPath = "/events/applyAsServer";
        private const string SaveResultPath = "/events/saveEventInstanceResult";
        private const string GetDataPath = "/events/getData";


        private float _currentEventStatusRefreshTimer;
        public EventInfo EventInfo => _eventInfo;
        public ObservableCollection<EventInfo> EventInfos => _eventInfos;


        private void Update()
        {
            _currentEventStatusRefreshTimer -= Time.deltaTime;
            if (!(_currentEventStatusRefreshTimer <= 0)) return;
            RefreshEventsStatuses();
            _currentEventStatusRefreshTimer = eventsStatusRefreshInterval;
        }

        public void Register(List<long> unitsIds, EventType type, Action<string> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                    RegisterPath,
                    new EventRegisterRequestDto
                    {
                        eventType = type,
                        unitsIds = unitsIds
                    },
                    ApiAdapter.Post,
                    null,
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
            _eventInfo = dto != null ? new EventInfo(dto.id, dto.eventType, dto.eventId, dto.host, dto.port) : null;
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
                        { eventInstanceId = EventInfo.EventInstanceId },
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

        private void RefreshEventsStatuses()
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ListResponseDto<EventInstanceDto>>(
                    StatusPath,
                    null,
                    ApiAdapter.Get,
                    OnRefreshEventsSuccess,
                    OnError));
        }

        private void OnRefreshEventsSuccess(ListResponseDto<EventInstanceDto> listDto)
        {
            if (listDto == null || !listDto.items.Any()) return;
            foreach (var eventInstanceDto in listDto.items.Where(
                         eventDto => EventInfos.All(ei => ei.EventInstanceId != eventDto.id)))
            {
                EventInfos.Add(new EventInfo(eventInstanceDto.id, eventInstanceDto.eventType,
                    eventInstanceDto.eventId, eventInstanceDto.host, eventInstanceDto.port));
            }
        }

        private void OnError(ErrorResponseDto obj)
        {
            Debug.LogError(obj.message);
        }
    }
}