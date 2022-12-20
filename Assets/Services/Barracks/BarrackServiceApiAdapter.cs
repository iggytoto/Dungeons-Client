using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Services.Common.Dto;
using Services.Dto;

namespace Services.Barracks
{
    public class BarrackServiceApiAdapter : ApiAdapterBase
    {
        private const string GetAvailableUnitsPath = "/barrack/availableUnits";
        private const string TrainUnitPath = "/barrack/trainUnit";

        public void GetAvailableUnits(UserContext loginServiceUserContext,
            EventHandler<UnitListResponseDto> successHandler, EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + GetAvailableUnitsPath, null,
                Get, new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                successHandler,
                errorHandler));
        }

        public void TrainUnit(long unitId, UserContext loginServiceUserContext,
            EventHandler<TrainUnitResponse> successHandler, EventHandler<ErrorResponseDto> errorHandler)
        {
            var requestBody = JsonConvert.SerializeObject(new TrainUnitRequest
            {
                unitId = unitId
            });
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + TrainUnitPath, requestBody,
                Get, new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                successHandler,
                errorHandler));
        }
    }
}