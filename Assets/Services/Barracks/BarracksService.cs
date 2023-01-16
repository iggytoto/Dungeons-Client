using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Items;
using Model.Units;
using Services;
using Services.Common.Dto;
using Services.Common.Dto.Items;
using Services.Dto;
using Unity.VisualScripting;
using UnityEngine;

public class BarracksService : ServiceBase, IBarrackService
{
    private const string GetAvailableUnitsPath = "/barrack/availableUnits";
    private const string GetAvailableItemsPath = "/barrack/availableItems";
    private const string ChangeUnitNamePath = "/barrack/changeUnitName";
    private const string ChangeUnitBattleBehaviorPath = "/barrack/changeUnitBattleBehavior";
    private const string UpgradeUnitSkillsPath = "/barrack/upgradeUnitSkills";
    private const string EquipItemPath = "/barrack/equip";
    private const string UnEquipItemPath = "/barrack/unEquip";

    public ObservableCollection<Unit> AvailableUnits { get; } = new();
    public ObservableCollection<Item> AvailableItems { get; } = new();


    [SerializeField] public float refreshInterval = 15;
    private float _refreshTimer;

    private void Update()
    {
        if (!(refreshInterval > 0)) return;
        _refreshTimer -= Time.deltaTime;
        if (_refreshTimer <= 0)
        {
            Refresh();
        }
    }

    private void Refresh()
    {
        StartCoroutine(APIAdapter.DoRequestCoroutine(
            APIAdapter.GetConnectionAddress() + GetAvailableUnitsPath,
            null,
            ApiAdapter.Get,
            OnGetAvailableUnitsSuccess,
            OnError,
            new UnitListResponseDtoDeserializer()));
        StartCoroutine(
            APIAdapter.DoRequestCoroutine<ListResponseDto<ItemDto>>(
                APIAdapter.GetConnectionAddress() + GetAvailableItemsPath,
                null,
                ApiAdapter.Get,
                OnGetAvailableItemsSuccess,
                OnError));
        _refreshTimer = refreshInterval;
    }

    private void OnGetAvailableUnitsSuccess(object sender, UnitListResponseDto e)
    {
        if (e == null)
        {
            return;
        }

        RefreshUnitsLocal(e.items?.Select(x => x?.ToDomain()));
    }

    private void OnGetAvailableItemsSuccess(object sender, ListResponseDto<ItemDto> listResponseDto)
    {
        if (listResponseDto?.items == null)
        {
            return;
        }

        RefreshItemsLocal(listResponseDto.items.Select(x => x?.ToDomain()));
    }

    private void RefreshUnitsLocal(IEnumerable<Unit> e)
    {
        AvailableUnits.Clear();
        if (e != null)
        {
            AvailableUnits.AddRange(e);
        }
    }

    private void RefreshItemsLocal(IEnumerable<Item> e)
    {
        AvailableItems.Clear();
        if (e != null)
        {
            AvailableItems.AddRange(e);
        }
    }

    private static void OnError(object sender, ErrorResponseDto e)
    {
        Debug.Log(e.message);
    }

    public ObservableCollection<Unit> Units => AvailableUnits;

    public void ChangeUnitName(long selectedUnitId, string newName)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                APIAdapter.GetConnectionAddress() + ChangeUnitNamePath,
                ApiAdapter.SerializeDto(new ChangeUnitNameRequestDto
                    { unitId = selectedUnitId, newName = newName }),
                ApiAdapter.Post,
                (_, _) => Refresh(),
                null));
    }

    public void ChangeUnitBattleBehavior(long selectedUnitId, BattleBehavior bb)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                APIAdapter.GetConnectionAddress() + ChangeUnitBattleBehaviorPath,
                ApiAdapter.SerializeDto(new ChangeUnitBattleBehaviorRequestDto
                    { unitId = selectedUnitId, newBattleBehavior = bb }),
                ApiAdapter.Post,
                (_, _) => Refresh(),
                null));
    }

    public void EquipItem(Item item, Unit unit)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                APIAdapter.GetConnectionAddress() + EquipItemPath,
                ApiAdapter.SerializeDto(new EquipItemRequestDto { itemId = item.Id, unitId = unit.Id }),
                ApiAdapter.Post,
                null,
                OnError));
    }

    public void UnEquipItem(Item item)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                APIAdapter.GetConnectionAddress() + UnEquipItemPath,
                ApiAdapter.SerializeDto(new UnEquipItemRequestDto { itemId = item.Id }),
                ApiAdapter.Post,
                null,
                OnError));
    }

    public void UpgradeUnitSkill<TDomain, TDto>(
        long skillId,
        UnitType unitType,
        string upgradeParamName,
        EventHandler<TDomain> onSuccess,
        Func<TDto, TDomain> dtoMapper)
        where TDomain : Skills
        where TDto : SkillsDto
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                APIAdapter.GetConnectionAddress() + UpgradeUnitSkillsPath,
                ApiAdapter.SerializeDto(new UpgradeUnitSkillRequestDto
                {
                    skillsId = skillId,
                    unitType = unitType,
                    paramNameToUpgrade = upgradeParamName
                }),
                ApiAdapter.Post,
                (_, dto) => onSuccess.Invoke(this, dtoMapper.Invoke(dto)),
                null,
                UnitSkillsDeserializerBase.GetDeserializer<TDto>(unitType)));
    }
}