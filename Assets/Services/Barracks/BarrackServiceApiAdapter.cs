using System;
using System.Collections.Generic;
using Services.Common.Dto;
using Services.Dto;

namespace Services.Barracks
{
    public class BarrackServiceApiAdapter : ApiAdapterBase
    {
        private const string GetAvailableUnitsPath = "/barrack/availableUnits";
        private const string ChangeUnitNamePath = "/barrack/changeUnitName";
        private const string ChangeUnitBattleBehaviorPath = "/barrack/changeUnitBattleBehavior";
        private const string UpgradeUnitSkillsPath = "/barrack/upgradeUnitSkills";

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
                    errorHandler,
                    new UnitListResponseDtoDeserializer()));
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

        public void ChangeUnitBattleBehavior(
            UserContext loginServiceUserContext,
            ChangeUnitBattleBehaviorRequestDto changeUnitBattleBehaviorRequestDto,
            EventHandler<ResponseBaseDto> successHandler)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + ChangeUnitBattleBehaviorPath,
                    SerializeDto(changeUnitBattleBehaviorRequestDto),
                    Post,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    successHandler,
                    null));
        }

        public void UpgradeUnitSkills<TDto>(
            UserContext userContext,
            UpgradeUnitSkillRequestDto dto,
            EventHandler<TDto> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
            where TDto : SkillsDto
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + UpgradeUnitSkillsPath,
                    SerializeDto(dto),
                    Post,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(userContext) } },
                    successHandler,
                    errorHandler,
                    UnitSkillsDeserializerBase.GetDeserializer<TDto>(dto.unitType)));
        }
    }
}