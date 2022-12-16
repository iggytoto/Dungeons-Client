using System;
using System.Collections.Generic;
using Services.Common.Dto;
using Services.Dto;
using Services.UnitShop;
using UnityEngine;

namespace Services.Barracks
{
    public class BarrackServiceApiAdapter : ApiAdapterBase
    {
        private const string Port = ":8080";
        private const string GetAvailableUnitsPath = "/barrack/availableUnits";
        private const string TrainUnitPath = "/barrack/trainUnit";


        // ReSharper disable Unity.PerformanceAnalysis
        public void GetAvailableUnits(UserContext loginServiceUserContext,
            EventHandler<UnitListResponseDto> successHandler, EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + GetAvailableUnitsPath, null,
                Get, new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                successHandler,
                errorHandler));
        }

        public void TrainUnit(long unitId, UserContext loginServiceUserContext,
            EventHandler<TrainUnitResponse> successHandler, EventHandler<ErrorResponseDto> errorHandler)
        {
            var requestBody = JsonUtility.ToJson(new TrainUnitRequest
            {
                unitId = unitId
            });
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + TrainUnitPath, requestBody,
                Get, new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                successHandler,
                errorHandler));
        }
    }
}