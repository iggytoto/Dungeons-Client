using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Units;
using Services;
using Services.Barracks;
using Services.Common.Dto;
using Services.Dto;
using Unity.VisualScripting;
using UnityEngine;

public class BarracksService : ServiceBase, IBarrackService
{
    public ObservableCollection<Unit> AvailableUnits { get; } = new();


    [SerializeField] public float refreshInterval = 15;
    private ILoginService _loginService;
    private BarrackServiceApiAdapter _apiAdapter;
    private float _refreshTimer;

    public void TrainUnit(long selectedUnitId)
    {
        _apiAdapter.TrainUnit(
            new TrainUnitRequest { unitId = selectedUnitId },
            _loginService.UserContext,
            OnTrainSuccess,
            OnError);
    }

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

        _apiAdapter.GetAvailableUnits(_loginService.UserContext, OnGetSuccess, OnError);
        _refreshTimer = refreshInterval;
    }

    private void OnGetSuccess(object sender, UnitListResponseDto e)
    {
        if (e == null)
        {
            return;
        }

        RefreshLocal(e.units?.Select(x => x?.ToDomain()));
    }

    private void RefreshLocal(IEnumerable<Unit> e)
    {
        AvailableUnits.Clear();
        if (e != null)
        {
            AvailableUnits.AddRange(e);
        }
    }

    private static void OnError(object sender, ErrorResponseDto e)
    {
        Debug.Log(e.message);
    }

    private void OnTrainSuccess(object sender, TrainUnitResponse e)
    {
        Refresh();
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

    public void UpgradeUnitEquipment<TDomain, TDto>(
        long equipmentId,
        UnitType unitType,
        string upgradeParamName,
        EventHandler<TDomain> onSuccess,
        Func<TDto, TDomain> dtoMapper)
        where TDomain : Equipment
        where TDto : EquipmentDto
    {
        _apiAdapter.UpgradeUnitEquipment<TDto>(
            _loginService.UserContext,
            new UpgradeUnitEquipmentRequestDto
            {
                equipmentId = equipmentId,
                unitType = unitType,
                paramNameToUpgrade = upgradeParamName
            },
            (_, dto) => onSuccess.Invoke(this, dtoMapper.Invoke(dto)),
            null);
    }
}