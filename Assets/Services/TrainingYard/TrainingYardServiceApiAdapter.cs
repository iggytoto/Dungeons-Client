using System;
using System.Collections.Generic;
using System.Linq;
using Services.Common;
using Services.Common.Dto;
using Services.Dto;
using Services.UnitShop;
using UnityEngine;

namespace Services.TrainingYard
{
    public class TrainingYardServiceApiAdapter : ApiAdapterBase
    {
        private const string GetRosterForUserPath = "/trainingYard/getRosterForUser";
        private const string SaveRostersPath = "/trainingYard/saveRosters";
        private const string SaveMatchResultPath = "/trainingYard/saveMatchResult";
        private const string Port = ":8080";

        public void GetRosterForUser(
            long userId,
            EventHandler<UnitListResponseDto> onSuccessHandler,
            EventHandler<ErrorResponseDto> onErrorHandler,
            UserContext context)
        {
            var requestData = JsonUtility.ToJson(new UserIdRequestDto
            {
                userId = userId
            });
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + GetRosterForUserPath, requestData, Get,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } },
                onSuccessHandler,
                onErrorHandler));
        }

        public void SaveRosters(IEnumerable<UnitDto> units,
            UserContext context)
        {
            var requestData = JsonUtility.ToJson(
                new UnitListRequestDto
                {
                    units = units.ToList()
                }
            );
            StartCoroutine(DoRequestCoroutine<ResponseBaseDto>(endpointAddress + Port + SaveRostersPath, requestData,
                Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(context) } },
                null,
                null));
        }

        public void SaveMatchResult(MatchResultDto result, UserContext loginServiceUserContext)
        {
            var requestData = JsonUtility.ToJson(result);
            StartCoroutine(DoRequestCoroutine<ResponseBaseDto>(endpointAddress + Port + SaveMatchResultPath, requestData,
                Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                null,
                null));
        }
    }
}