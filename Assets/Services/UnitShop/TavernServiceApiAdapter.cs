using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Services.Common.Dto;
using Services.Dto;

namespace Services.UnitShop
{
    public class TavernServiceApiAdapter : ApiAdapterBase
    {
        private const string GetAvailableUnitsPath = "/tavern/availableUnits";
        private const string BuyUnitPath = "/tavern/buyUnit";

        public void GetAvailableUnits(
            UserContext loginServiceUserContext,
            EventHandler<IEnumerable<UnitForSale>> successHandler,
            EventHandler<string> errorHandler)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + GetAvailableUnitsPath,
                    null,
                    Get,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    (_, dto) => successHandler?.Invoke(this, dto.units.Select(uDto => uDto.ToUnitForSale())),
                    (_, dto) => errorHandler?.Invoke(this, dto.message),
                    new UnitListResponseDtoDeserializer()));
        }

        public void BuyUnit(
            Unit unit,
            UserContext loginServiceUserContext,
            EventHandler<Unit> successHandler,
            EventHandler<string> errorHandler)
        {
            var requestBody = JsonConvert.SerializeObject(new BuyUnitRequest
            {
                type = unit.type
            });
            StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + BuyUnitPath, requestBody, Post,
                new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                (_, dto) => successHandler?.Invoke(this, dto.ToDomain()),
                (_, dto) => errorHandler?.Invoke(this, dto.message),
                new UnitDtoDeserializer()));
        }
    }
}