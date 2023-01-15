using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Items;
using Model.Units;
using Services;
using Services.Barracks;
using Services.Common.Dto;
using Services.Common.Dto.Items;
using Services.Dto;
using Unity.VisualScripting;
using UnityEngine;

public class BarracksService : ServiceBase, IBarrackService
{
    public ObservableCollection<Unit> AvailableUnits { get; } = new();
    public ObservableCollection<Item> AvailableItems { get; } = new();


    [SerializeField] public float refreshInterval = 15;
    private ILoginService _loginService;
    private BarrackServiceApiAdapter _apiAdapter;
    private float _refreshTimer;

    public override void InitService()
    {
        _loginService = FindObjectOfType<GameService>().LoginService;
        _apiAdapter = gameObject.AddComponent<BarrackServiceApiAdapter>();
        _apiAdapter.endpointHttp = EndpointHttp;
        _apiAdapter.endpointAddress = EndpointHost;
        _apiAdapter.endpointPort = EndpointPrt;
        Debug.Log(
            $"Barrack service adapter configured with endpoint:{_apiAdapter.GetConnectionAddress()}");
    }

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
        if (_loginService == null)
        {
            _refreshTimer = long.MaxValue;
            Debug.LogError("Login Service dependency not set for UnitShopService");
            return;
        }

        if (_loginService.UserContext == null)
        {
            Debug.LogWarning("User context is not set cannot do without user context");
        }

        _apiAdapter.GetAvailableUnits(_loginService.UserContext, OnGetAvailableUnitsSuccess, OnError);
        _apiAdapter.GetAvailableItems(_loginService.UserContext, OnGetAvailableItemsSuccess, OnError);
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
        _apiAdapter.ChangeUnitName(
            _loginService.UserContext,
            new ChangeUnitNameRequestDto { unitId = selectedUnitId, newName = newName },
            (_, _) => Refresh());
    }

    public void ChangeUnitBattleBehavior(long selectedUnitId, BattleBehavior bb)
    {
        _apiAdapter.ChangeUnitBattleBehavior(
            _loginService.UserContext,
            new ChangeUnitBattleBehaviorRequestDto { unitId = selectedUnitId, newBattleBehavior = bb },
            (_, _) => Refresh());
    }

    public void EquipItem(Item item, Unit unit)
    {
        _apiAdapter.EquipItem(_loginService.UserContext, new EquipItemRequestDto { itemId = item.Id, unitId = unit.Id },
            null, OnError);
    }

    public void UnEquipItem(Item item)
    {
        _apiAdapter.UnEquipItem(_loginService.UserContext, new UnEquipItemRequestDto { itemId = item.Id }, null,
            OnError);
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
        _apiAdapter.UpgradeUnitSkills<TDto>(
            _loginService.UserContext,
            new UpgradeUnitSkillRequestDto
            {
                skillsId = skillId,
                unitType = unitType,
                paramNameToUpgrade = upgradeParamName
            },
            (_, dto) => onSuccess.Invoke(this, dtoMapper.Invoke(dto)),
            null);
    }
}