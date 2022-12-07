using System;
using System.Collections.Generic;
using Services.Dto;
using Services.UnitShop;
using UnityEngine;

namespace Services.Barracks
{
    public class BarrackServiceApiAdapter : ApiAdapterBase
    {
        private const string Port = ":8082";
        private const string GetAvailableUnitsPath = "/barrack/availableUnits";
        private const string TrainUnitPath = "/barrack/trainUnit";


        // ReSharper disable Unity.PerformanceAnalysis
        public void GetAvailableUnits(UserContext loginServiceUserContext,
            EventHandler<GetAvailableUnitsResponse> successHandler, EventHandler<ErrorResponse> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + GetAvailableUnitsPath, null,
                Get, new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                successHandler,
                errorHandler));
        }

        public void TrainUnit(long unitId, UserContext loginServiceUserContext,
            EventHandler<TrainUnitResponse> successHandler, EventHandler<ErrorResponse> errorHandler)
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