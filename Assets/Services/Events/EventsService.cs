using System;
using System.Collections.Generic;
using System.Linq;
using Model.Events;
using Services.Common.Dto;
using Services.Dto;
using Services.Events.Dto;
using UnityEngine;
using Event = Model.Events.Event;
using EventType = Model.Events.EventType;

namespace Services.Events
{
    public class EventsService : ServiceBase, IEventsService
    {
        private const string RegisterPath = "/events/register";
        private const string StatusPath = "/events/status";
        private const string ApplyAsServerPath = "/events/applyAsServer";
        private const string SaveResultPath = "/events/saveEventInstanceResult";
        private const string GetDataPath = "/events/getData";

        public EventInfo EventInfo { get; private set; }

        public void Register(List<long> unitsIds, EventType type, EventHandler<ErrorResponseDto> onError)
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
                    onError));
        }

        public void Status(EventHandler<List<Event>> onSuccessHandler, EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ListResponseDto<EventDto>>(
                    StatusPath,
                    null,
                    ApiAdapter.Get,
                    (_, dto) => onSuccessHandler?.Invoke(this, dto.items.Select(d => d.toDomain()).ToList()),
                    onError));
        }

        public void ApplyAsServer(
            string host,
            string port,
            EventHandler<EventInstance> onSuccessHandler,
            EventHandler<ErrorResponseDto> onError)
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
                    (_, dto) => OnSuccessApplyAsServer(onSuccessHandler, dto),
                    onError));
        }

        private void OnSuccessApplyAsServer(EventHandler<EventInstance> onSuccessHandler, EventInstanceDto dto)
        {
            EventInfo = new EventInfo(dto.id, dto.eventType);
            onSuccessHandler?.Invoke(this, dto.ToDomain());
        }

        public void GetEventInstanceRosters(EventHandler<List<Unit>> onSuccessHandler,
            EventHandler<ErrorResponseDto> onError)
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
                    (_, dto) => onSuccessHandler?.Invoke(this, dto.items.Select(d => d.ToDomain()).ToList()),
                    onError));
        }

        public void SaveResult(EventInstanceResult result, EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                    SaveResultPath,
                    EventInstanceResultDto.FromDomain(result),
                    ApiAdapter.Post,
                    OnSuccessSaveResult,
                    onError));
        }

        private void OnSuccessSaveResult(object sender, ResponseBaseDto e)
        {
            EventInfo = null;
        }
    }
}