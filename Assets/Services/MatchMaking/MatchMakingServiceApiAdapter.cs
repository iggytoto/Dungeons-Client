using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    public class MatchMakingServiceApiAdapter : ApiAdapterBase
    {
        private const string RegisterPath = "/matchMaking/register";
        private const string CancelPath = "/matchMaking/cancel";
        private const string StatusPath = "/matchMaking/status";
        private const string ApplyAsServerPath = "/matchMaking/applyServer";

        public void Register(IEnumerable<long> rosterUnitsIds, UserContext context,
            EventHandler<MatchMakingStatusResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            var requestBody = JsonConvert.SerializeObject(new MatchMakingRegisterRequest()
            {
                rosterUnitsIds = new List<long>(rosterUnitsIds)
            });
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + RegisterPath, requestBody, Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } }, successHandler,
                errorHandler));
        }

        public void Cancel(UserContext context, EventHandler<EmptyResponseDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + CancelPath, null, Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } }, successHandler,
                errorHandler));
        }

        public void Status(UserContext context, EventHandler<MatchMakingStatusResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + StatusPath, null, Get,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } }, successHandler,
                errorHandler));
        }

        public void ApplyAsServer(string ip, string port, UserContext context,
            EventHandler<MatchMakingApplyAsServerResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            var requestBody = JsonConvert.SerializeObject(new MatchMakingApplyAsServerRequest()
            {
                ip = ip,
                port = port
            });
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + ApplyAsServerPath, requestBody, Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } }, successHandler,
                errorHandler));
        }
    }
}