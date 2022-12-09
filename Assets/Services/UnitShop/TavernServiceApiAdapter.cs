using System;
using System.Collections.Generic;
using Services.Dto;
using UnityEngine;

namespace Services.UnitShop
{
    public class TavernServiceApiAdapter : ApiAdapterBase
    {
        private const string Port = ":8080";
        private const string GetAvailableUnitsPath = "/tavern/availableUnits";
        private const string BuyUnitPath = "/tavern/buyUnit";

        public void GetAvailableUnits(UserContext loginServiceUserContext,
            EventHandler<GetAvailableUnitsResponse> successHandler,
            EventHandler<ErrorResponse> errorHandler)
        {
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + GetAvailableUnitsPath, null,
                Get, new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                successHandler,
                errorHandler));
        }


        public void BuyUnit(Unit unit,
            UserContext loginServiceUserContext,
            EventHandler<EmptyResponse> successHandler,
            EventHandler<ErrorResponse> errorHandler)
        {
            var requestBody = JsonUtility.ToJson(new BuyUnitRequest
            {
                unitId = unit.Id
            });
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + BuyUnitPath, requestBody, Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                successHandler,
                errorHandler));
        }
    }
}