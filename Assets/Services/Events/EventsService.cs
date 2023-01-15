using System;
using System.Collections.Generic;
using System.Linq;
using Model.Events;
using Services.Dto;
using Services.Events.Dto;
using UnityEngine;
using Event = Model.Events.Event;
using EventType = Model.Events.EventType;

namespace Services.Events
{
    public class EventsService : ServiceBase, IEventsService
    {
        private EventsServiceApiAdapter _apiAdapter;
        private ILoginService _loginService;

        public void Register(List<long> unitsIds, EventType type, EventHandler<ErrorResponseDto> onError)
        {
            _apiAdapter.Register(new EventRegisterRequestDto
                {
                    eventType = type,
                    unitsIds = unitsIds
                },
                _loginService.UserContext,
                onError);
        }

        public void Status(EventHandler<List<Event>> onSuccessHandler, EventHandler<ErrorResponseDto> onError)
        {
            _apiAdapter.Status(
                _loginService.UserContext,
                (_, dto) => onSuccessHandler?.Invoke(this, dto.items.Select(d => d.toDomain()).ToList()),
                onError
            );
        }

        public void ApplyAsServer(
            string host,
            string port,
            EventHandler<EventInstance> onSuccessHandler,
            EventHandler<ErrorResponseDto> onError)
        {
            _apiAdapter.ApplyAsServer(
                new ApplyAsEventProcessorDto { host = host, port = port },
                _loginService.UserContext,
                (_, dto) => onSuccessHandler?.Invoke(this, dto.ToDomain()),
                onError);
        }

        public void SaveResult(EventInstanceResult result, EventHandler<ErrorResponseDto> onError)
        {
            _apiAdapter.Save(
                EventInstanceResultDto.FromDomain(result),
                _loginService.UserContext,
                onError
            );
        }

        public override void InitService()
        {
            _loginService = FindObjectOfType<GameService>().LoginService;
            _apiAdapter = gameObject.AddComponent<EventsServiceApiAdapter>();
            _apiAdapter.endpointHttp = EndpointHttp;
            _apiAdapter.endpointAddress = EndpointHost;
            _apiAdapter.endpointPort = EndpointPrt;
            Debug.Log(
                $"Events service adapter configured with endpoint:{_apiAdapter.GetConnectionAddress()}");
        }
    }
}