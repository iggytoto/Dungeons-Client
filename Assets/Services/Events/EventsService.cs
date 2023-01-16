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

        private ILoginService _loginService;

        public void Register(List<long> unitsIds, EventType type, EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                    APIAdapter.GetConnectionAddress() + RegisterPath,
                    ApiAdapter.SerializeDto(new EventRegisterRequestDto
                    {
                        eventType = type,
                        unitsIds = unitsIds
                    }),
                    ApiAdapter.Post,
                    APIAdapter.GetAuthHeader(_loginService.UserContext),
                    null,
                    onError));
        }

        public void Status(EventHandler<List<Event>> onSuccessHandler, EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ListResponseDto<EventDto>>(
                    APIAdapter.GetConnectionAddress() + StatusPath,
                    null,
                    ApiAdapter.Get,
                    APIAdapter.GetAuthHeader(_loginService.UserContext),
                    (_, dto) => onSuccessHandler?.Invoke(this, dto.items.Select(d => d.toDomain()).ToList()),
                    onError));
        }

        public void ApplyAsServer(
            string host,
            string port,
            EventHandler<EventInstance> onSuccessHandler,
            EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<EventInstanceDto>(
                    APIAdapter.GetConnectionAddress() + ApplyAsServerPath,
                    ApiAdapter.SerializeDto(new ApplyAsEventProcessorDto { host = host, port = port }),
                    ApiAdapter.Post,
                    APIAdapter.GetAuthHeader(_loginService.UserContext),
                    (_, dto) => onSuccessHandler?.Invoke(this, dto.ToDomain()),
                    onError));
        }

        public void GetEventInstanceRosters(long eventInstanceId, EventHandler<List<Unit>> onSuccessHandler,
            EventHandler<ErrorResponseDto> onError)
        {
            throw new NotImplementedException();
        }

        public void SaveResult(EventInstanceResult result, EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                    APIAdapter.GetConnectionAddress() + SaveResultPath,
                    ApiAdapter.SerializeDto(EventInstanceResultDto.FromDomain(result)),
                    ApiAdapter.Post,
                    APIAdapter.GetAuthHeader(_loginService.UserContext),
                    null,
                    onError));
        }

        public new void InitService()
        {
            _loginService = FindObjectOfType<GameService>().LoginService;
            Debug.Log(
                $"Events service adapter configured with endpoint:{APIAdapter.GetConnectionAddress()}");
        }
    }
}