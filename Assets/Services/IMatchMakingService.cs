using System;
using System.Collections.Generic;
using Services.Common;
using Services.Dto;

namespace Services
{
    public interface IMatchMakingService : IService
    {
        public void Register(
            IEnumerable<long> roster,
            EventHandler<MatchDto> onSuccess,
            EventHandler<ErrorResponseDto> onError);

        public void Cancel();

        public void Status(
            EventHandler<MatchDto> onSuccess,
            EventHandler<ErrorResponseDto> onError);

        public void ApplyForServer(
            string address,
            string port,
            EventHandler<MatchDto> onSuccess,
            EventHandler<ErrorResponseDto> onError);
    }
}