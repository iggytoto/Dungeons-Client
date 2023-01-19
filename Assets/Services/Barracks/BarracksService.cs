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
    public ObservableCollection<Unit> Units => AvailableUnits;

    public event Action<Unit> UnitUpdated;
    public event Action<Item> ItemUnEquipped;
    public event Action<Item> ItemEquipped;


    [SerializeField] public float refreshInterval = 15;
    private float _refreshTimer;
    private ILoginService _loginService;


    private void Start()
    {
        _loginService = FindObjectOfType<GameService>().LoginService;
    }

    private void Update()
    {
        if (!(refreshInterval > 0) || _loginService.UserContext == null) return;
        _refreshTimer -= Time.deltaTime;
        if (_refreshTimer <= 0)
        {
            Refresh();
        }
    }

    private void Refresh()
    {
        StartCoroutine(APIAdapter.DoRequestCoroutine(
            GetAvailableUnitsPath,
            null,
            ApiAdapter.Get,
            OnGetAvailableUnitsSuccess,
            OnError,
            new DefaultListResponseDtoDeserializer<UnitDto>(new UnitDtoDeserializer())));
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                GetAvailableItemsPath,
                null,
                ApiAdapter.Get,
                OnGetAvailableItemsSuccess,
                OnError,
                new DefaultListResponseDtoDeserializer<ItemDto>(new DefaultDtoDeserializer<ItemDto>())));
        _refreshTimer = refreshInterval;
    }

    private void OnGetAvailableUnitsSuccess(ListResponseDto<UnitDto> listResponseDto)
    {
        if (listResponseDto == null)
        {
            return;
        }

        RefreshUnitsLocal(listResponseDto.items?.Select(x => x?.ToDomain()).ToList());
    }

    private void OnGetAvailableItemsSuccess(ListResponseDto<ItemDto> listResponseDto)
    {
        if (listResponseDto?.items == null)
        {
            return;
        }

        RefreshItemsLocal(listResponseDto.items.Select(x => x?.ToDomain()).ToList());
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

    private static void OnError(ErrorResponseDto e)
    {
        Debug.Log(e.message);
    }

    public void ChangeUnitName(long selectedUnitId, string newName)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                ChangeUnitNamePath,
                new ChangeUnitNameRequestDto
                    { unitId = selectedUnitId, newName = newName },
                ApiAdapter.Post,
                OnUnitUpdated,
                OnError,
                new UnitDtoDeserializer()));
    }

    public void ChangeUnitBattleBehavior(long selectedUnitId, BattleBehavior bb)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                ChangeUnitBattleBehaviorPath,
                new ChangeUnitBattleBehaviorRequestDto
                    { unitId = selectedUnitId, newBattleBehavior = bb },
                ApiAdapter.Post,
                OnUnitUpdated,
                OnError,
                new UnitDtoDeserializer()));
    }

    public void EquipItem(Item item, Unit unit)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine<ItemDto>(
                EquipItemPath,
                new EquipItemRequestDto { itemId = item.Id, unitId = unit.Id },
                ApiAdapter.Post,
                OnItemEquipped,
                OnError));
    }

    public void UnEquipItem(Item item)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine<ItemDto>(
                UnEquipItemPath,
                new UnEquipItemRequestDto { itemId = item.Id },
                ApiAdapter.Post,
                OnItemUnequipped,
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
                UpgradeUnitSkillsPath,
                new UpgradeUnitSkillRequestDto
                {
                    skillsId = skillId,
                    unitType = unitType,
                    paramNameToUpgrade = upgradeParamName
                },
                ApiAdapter.Post,
                dto => onSuccess.Invoke(this, dtoMapper.Invoke(dto)),
                OnError,
                UnitSkillsDeserializerBase.GetDeserializer<TDto>(unitType)));
    }

    private void OnItemUnequipped(ItemDto obj)
    {
        var unequippedItem = obj.ToDomain();
        var currentItem = AvailableItems.FirstOrDefault(i => i.Id == unequippedItem.Id);
        var unitWasEquipped = AvailableUnits.FirstOrDefault(u => u.Items.Any(i => i.Id == unequippedItem.Id));
        if (currentItem != null || unitWasEquipped == null) return;
        AvailableItems.Add(unequippedItem);
        unitWasEquipped.UnEquipItem(unequippedItem);
        UnitUpdated?.Invoke(unitWasEquipped);
        ItemUnEquipped?.Invoke(unequippedItem);
    }

    private void OnItemEquipped(ItemDto obj)
    {
        var equippedItem = obj.ToDomain();
        var currentItem = AvailableItems.FirstOrDefault(i => i.Id == equippedItem.Id);
        var unitToEquip = AvailableUnits.FirstOrDefault(u => equippedItem.UnitId == u.Id);
        if (currentItem == null || unitToEquip == null) return;
        unitToEquip.EquipItem(equippedItem);
        UnitUpdated?.Invoke(unitToEquip);
        ItemEquipped?.Invoke(equippedItem);
    }

    private void OnUnitUpdated(UnitDto obj)
    {
        var newUnit = obj.ToDomain();
        var oldUnit = AvailableUnits.FirstOrDefault(x => x.Id == newUnit.Id);
        if (oldUnit == null) return;
        AvailableUnits.Remove(oldUnit);
        AvailableUnits.Add(newUnit);
        UnitUpdated?.Invoke(newUnit);
    }
}