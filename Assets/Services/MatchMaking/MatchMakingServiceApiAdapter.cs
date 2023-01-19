using System;
using System.Collections.Generic;
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
            Action<MatchDto> successHandler,
            Action<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    RegisterPath,
                    new MatchMakingRegisterRequestDto()
                    {
                        rosterUnitsIds = new List<long>(rosterUnitsIds)
                    },
                    Post,
                    successHandler,
                    errorHandler));
        }

        public void Cancel(UserContext context, Action<EmptyResponseDto> successHandler,
            Action<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(CancelPath, null, Post,
                successHandler,
                errorHandler));
        }

        public void Status(UserContext context,
            Action<MatchDto> successHandler,
            Action<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(StatusPath, null, Get,
                successHandler,
                errorHandler));
        }

        public void ApplyAsServer(string ip, string port, UserContext context,
            Action<MatchMakingApplyAsServerResponseDto> successHandler,
            Action<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(ApplyAsServerPath,
                new MatchMakingApplyAsServerRequestDto()
                {
                    ip = ip,
                    port = port
                }, Post,
                successHandler,
                errorHandler));
        }
    }
}