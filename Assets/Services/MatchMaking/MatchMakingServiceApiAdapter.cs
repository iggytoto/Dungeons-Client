using System;
using System.Collections.Generic;
using Services.Dto;
using UnityEngine;

namespace Services.MatchMaking
{
    public class MatchMakingServiceApiAdapter : ApiAdapterBase
    {
        private const string RegisterPath = "/matchMaking/register";
        private const string CancelPath = "/matchMaking/cancel";
        private const string StatusPath = "/matchMaking/status";
        private const string ApplyAsServerPath = "/matchMaking/applyServer";
        private const string Port = ":8080";

        public void Register(IEnumerable<long> rosterUnitsIds, UserContext context,
            EventHandler<EmptyResponseDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            var requestBody = JsonUtility.ToJson(new MatchMakingRegisterRequest()
            {
                rosterUnitsIds = new List<long>(rosterUnitsIds)
            });
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + RegisterPath, requestBody, Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } }, successHandler,
                errorHandler));
        }

        public void Cancel(UserContext context, EventHandler<EmptyResponseDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + CancelPath, null, Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } }, successHandler,
                errorHandler));
        }

        public void Status(UserContext context, EventHandler<MatchMakingStatusResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + StatusPath, null, Get,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } }, successHandler,
                errorHandler));
        }

        public void ApplyAsServer(string ip, string port, UserContext context,
            EventHandler<MatchMakingApplyAsServerResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            var requestBody = JsonUtility.ToJson(new MatchMakingApplyAsServerRequest()
            {
                ip = ip,
                port = port
            });
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + ApplyAsServerPath, requestBody, Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } }, successHandler,
                errorHandler));
        }
    }
}