using System;
using System.Collections.Generic;
using Services.Common.Dto;
using Services.Dto;

namespace Services.Barracks
{
    public class BarrackServiceApiAdapter : ApiAdapterBase
    {
        private const string GetAvailableUnitsPath = "/barrack/availableUnits";
        private const string TrainUnitPath = "/barrack/trainUnit";
        private const string ChangeUnitNamePath = "/barrack/changeUnitPath";

        public void GetAvailableUnits(
            UserContext loginServiceUserContext,
            EventHandler<UnitListResponseDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + GetAvailableUnitsPath,
                    null,
                    Get,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    successHandler,
                    errorHandler));
        }

        public void TrainUnit(
            TrainUnitRequest dto,
            UserContext loginServiceUserContext,
            EventHandler<TrainUnitResponse> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + TrainUnitPath,
                    SerializeDto(dto),
                    Get,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    successHandler,
                    errorHandler));
        }

        public void ChangeUnitName(
            UserContext loginServiceUserContext,
            ChangeUnitNameRequestDto changeUnitNameRequestDto,
            EventHandler<ResponseBaseDto> successHandler)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + ChangeUnitNamePath,
                    SerializeDto(changeUnitNameRequestDto),
                    Post,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    successHandler,
                    null));
        }
    }
}