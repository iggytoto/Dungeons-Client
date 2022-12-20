using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Services.Common;
using Services.Common.Dto;
using Services.Dto;

namespace Services.TrainingYard
{
    public class TrainingYardServiceApiAdapter : ApiAdapterBase
    {
        private const string GetRosterForUserPath = "/training/getRosterForUser";
        private const string SaveRostersPath = "/training/saveRosters";
        private const string SaveMatchResultPath = "/training/saveMatchResult";

        public void GetRosterForUser(
            long userId,
            EventHandler<UnitListResponseDto> onSuccessHandler,
            EventHandler<ErrorResponseDto> onErrorHandler,
            UserContext context)
        {
            var requestData = JsonConvert.SerializeObject(new UserIdRequestDto
            {
                userId = userId
            });
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + GetRosterForUserPath, requestData, Get,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } },
                onSuccessHandler,
                onErrorHandler));
        }

        public void SaveRosters(IEnumerable<UnitDto> units,
            UserContext context)
        {
            var requestData = JsonConvert.SerializeObject(
                new UnitListRequestDto
                {
                    units = units.ToList()
                }
            );
            StartCoroutine(DoRequestCoroutine<ResponseBaseDto>(GetConnectionAddress() + SaveRostersPath, requestData,
                Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } },
                null,
                null));
        }

        public void SaveMatchResult(MatchResultDto result, UserContext loginServiceUserContext)
        {
            var requestData = JsonConvert.SerializeObject(result);
            StartCoroutine(DoRequestCoroutine<ResponseBaseDto>(GetConnectionAddress() + SaveMatchResultPath, requestData,
                Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                null,
                null));
        }
    }
}