using System;
using System.Collections.Generic;
using Services.Common.Dto;
using Services.Common.Dto.Items;
using Services.Dto;

namespace Services.Barracks
{
    public class BarrackServiceApiAdapter : ApiAdapterBase
    {
        private const string GetAvailableUnitsPath = "/barrack/availableUnits";
        private const string GetAvailableItemsPath = "/barrack/availableItems";
        private const string ChangeUnitNamePath = "/barrack/changeUnitName";
        private const string ChangeUnitBattleBehaviorPath = "/barrack/changeUnitBattleBehavior";
        private const string UpgradeUnitSkillsPath = "/barrack/upgradeUnitSkills";
        private const string EquipItemPath = "/barrack/equip";
        private const string UnEquipItemPath = "/barrack/unEquip";

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

        public void GetAvailableItems(
            UserContext loginServiceUserContext,
            EventHandler<ListResponseDto<ItemDto>> successHandler,
            EventHandler<ErrorResponseDto> errorHandler)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + GetAvailableItemsPath,
                    null,
                    Get,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    successHandler,
                    errorHandler));
        }

        public void EquipItem(
            UserContext loginServiceUserContext,
            EquipItemRequestDto equipItemRequestDto,
            EventHandler<ResponseBaseDto> onSuccess,
            EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + EquipItemPath,
                    SerializeDto(equipItemRequestDto),
                    Post,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    onSuccess,
                    onError));
        }

        public void UnEquipItem(
            UserContext loginServiceUserContext,
            UnEquipItemRequestDto unEquipItemRequestDto,
            EventHandler<ResponseBaseDto> onSuccess,
            EventHandler<ErrorResponseDto> onError)
        {
            StartCoroutine(
                DoRequestCoroutine(
                    GetConnectionAddress() + UnEquipItemPath,
                    SerializeDto(unEquipItemRequestDto),
                    Post,
                    new Dictionary<string, string> { { Authorization, GetTokenValueHeader(loginServiceUserContext) } },
                    onSuccess,
                    onError));
        }
    }
}