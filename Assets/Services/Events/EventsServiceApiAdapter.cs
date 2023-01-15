using System;
using System.Collections.Generic;
using Services.Common.Dto;
using Services.Dto;
using Services.Events.Dto;

namespace Services.Events
{
    public class EventsServiceApiAdapter : ApiAdapterBase
    {
        private const string RegisterPath = "/events/register";
        private const string StatusPath = "/events/status";
        private const string ApplyAsServerPath = "/events/applyAsServer";
        private const string SaveResultPath = "/events/saveEventInstanceResult";

        public void Register(EventRegisterRequestDto dto
            , UserContext loginServiceUserContext,
            EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                DoRequestCoroutine<ResponseBaseDto>(
                    GetConnectionAddress() + RegisterPath,
                    SerializeDto(dto),
                    Post,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    null,
                    onError));
        }

        public void Status(
            UserContext loginServiceUserContext,
            EventHandler<ListResponseDto<EventDto>> onSuccess,
            EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + StatusPath,
                    null,
                    Get,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    onSuccess,
                    onError));
        }

        public void ApplyAsServer(ApplyAsEventProcessorDto dto,
            UserContext loginServiceUserContext,
            EventHandler<EventInstanceDto> onSuccess,
            EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + ApplyAsServerPath,
                    SerializeDto(dto),
                    Post,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    onSuccess,
                    onError));
        }

        public void Save(EventInstanceResultDto dto, UserContext loginServiceUserContext,
            EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                DoRequestCoroutine<ResponseBaseDto>(
                    GetConnectionAddress() + SaveResultPath,
                    SerializeDto(dto),
                    Post,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    null,
                    onError));
        }
    }
}