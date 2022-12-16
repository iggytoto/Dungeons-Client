using System;
using System.Collections.Generic;
using Services.Common.Dto;
using Services.Dto;
using UnityEngine;

namespace Services.UnitShop
{
    public class TavernServiceApiAdapter : ApiAdapterBase
    {
        private const string GetAvailableUnitsPath = "/tavern/availableUnits";
        private const string BuyUnitPath = "/tavern/buyUnit";

        public void GetAvailableUnits(UserContext loginServiceUserContext,
            EventHandler<UnitListResponseDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + GetAvailableUnitsPath, null,
                Get, new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                successHandler,
                errorHandler));
        }


        public void BuyUnit(Unit unit,
            UserContext loginServiceUserContext,
            EventHandler<EmptyResponseDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            var requestBody = JsonUtility.ToJson(new BuyUnitRequest
            {
                unitId = unit.Id
            });
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + BuyUnitPath, requestBody, Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                successHandler,
                errorHandler));
        }
    }
}