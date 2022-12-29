using System;
using System.Collections.Generic;
using Services.Common;
using Services.Common.Dto;
using Services.Dto;

namespace Services.TrainingYard
{
    public class TrainingYardServiceApiAdapter : ApiAdapterBase
    {
        private const string GetRosterForUserPath = "/training/getRosterForUser";
        private const string SaveMatchResultPath = "/training/saveTrainingResult";

        public void GetRosterForUser(
            UserIdRequestDto dto,
            EventHandler<UnitListResponseDto> onSuccessHandler,
            EventHandler<ErrorResponseDto> onErrorHandler,
            UserContext context)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + GetRosterForUserPath,
                    SerializeDto(dto),
                    Get,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } },
                    onSuccessHandler,
                    onErrorHandler));
        }

        public void SaveMatchResult(
            MatchResultDto result,
            UserContext loginServiceUserContext)
        {
            StartCoroutine(DoRequestCoroutine<ResponseBaseDto>(GetConnectionAddress() + SaveMatchResultPath,
                SerializeDto(result),
                Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                null,
                null));
        }
    }
}