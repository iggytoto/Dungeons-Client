using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    public class MatchMakingServiceApiAdapter : ApiAdapter
    {
        private const string RegisterPath = "/matchMaking/register";
        private const string CancelPath = "/matchMaking/cancel";
        private const string StatusPath = "/matchMaking/status";
        private const string ApplyAsServerPath = "/matchMaking/applyServer";

        public void Register(IEnumerable<long> rosterUnitsIds, UserContext context,
            EventHandler<MatchDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + RegisterPath, new MatchMakingRegisterRequestDto()
                {
                    rosterUnitsIds = new List<long>(rosterUnitsIds)
                }, Post,
                successHandler,
                errorHandler));
        }

        public void Cancel(UserContext context, EventHandler<EmptyResponseDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + CancelPath, null, Post,
                successHandler,
                errorHandler));
        }

        public void Status(UserContext context,
            EventHandler<MatchDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + StatusPath, null, Get,
                successHandler,
                errorHandler));
        }

        public void ApplyAsServer(string ip, string port, UserContext context,
            EventHandler<MatchMakingApplyAsServerResponseDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + ApplyAsServerPath, new MatchMakingApplyAsServerRequestDto()
                {
                    ip = ip,
                    port = port
                }, Post,
                successHandler,
                errorHandler));
        }
    }
}