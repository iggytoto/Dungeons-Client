using System;
using System.Collections.Generic;
using Services.Dto;
using Services.Login;
using UnityEngine;

namespace Services.UnitShop
{
    public class TavernServiceApiAdapter : ApiAdapterBase
    {
        private const string Port = ":8081";
        private const string GetAvailableUnitsPath = "/tavern/availableUnits";
        private const string BuyUnitPath = "/tavern/buyUnit";
        private const string Authorization = "Authorization";
        private const string Bearer = "Bearer ";


        public void GetAvailableUnits(UserContext loginServiceUserContext,
            EventHandler<GetAvailableUnitsResponse> successHandler,
            EventHandler<ErrorResponse> errorHandler)
        {
            var token = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(new TokenDto
                {
                    value = loginServiceUserContext.value,
                    userId = loginServiceUserContext.userId
                })));
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + GetAvailableUnitsPath, null,
                Get, new Dictionary<string, string> { { Authorization, Bearer + token } },
                successHandler,
                errorHandler));
        }


        public void BuyUnit(Unit unit,
            UserContext loginServiceUserContext,
            EventHandler<BuyUnitResponse> successHandler,
            EventHandler<ErrorResponse> errorHandler)
        {
            var requestBody = JsonUtility.ToJson(new BuyUnitRequest
            {
                unitId = unit.Id
            });
            var token = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(loginServiceUserContext)));
            StartCoroutine(DoRequestCoroutine(endpointAddress + Port + BuyUnitPath, requestBody, Post,
                new Dictionary<string, string> { { Authorization, Bearer + token } },
                successHandler,
                errorHandler));
        }
    }
}