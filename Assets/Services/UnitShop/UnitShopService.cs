using System.Collections.ObjectModel;
using System.Linq;
using Services;
using Services.Common.Dto;
using Services.Dto;
using Services.UnitShop;
using Unity.VisualScripting;
using UnityEngine;

public class UnitShopService : ServiceBase, ITavernService
{
    [SerializeField] public float shopRefreshInterval = 15;

    private ILoginService _loginService;
    private TavernServiceApiAdapter _apiAdapter;
    private float _shopRefreshTimer;
    private ObservableCollection<Unit> _availableUnits;

    public ObservableCollection<UnitForSale> AvailableUnits { get; } = new();
    

    public override void InitService()
    {
        _loginService = FindObjectOfType<GameService>().LoginService;
        _apiAdapter = gameObject.AddComponent<TavernServiceApiAdapter>();
        _apiAdapter.endpointHttp = EndpointHttp;
        _apiAdapter.endpointAddress = EndpointHost;
        _apiAdapter.endpointPort = EndpointPrt;
        Debug.Log(
            $"Tavern service adapter configured with endpoint:{_apiAdapter.GetConnectionAddress()}");
    }

    private void Update()
    {
        if (shopRefreshInterval > 0)
        {
            _shopRefreshTimer -= Time.deltaTime;
            if (_shopRefreshTimer <= 0)
            {
                RefreshShop();
            }
        }
    }

    private void RefreshShop()
    {
        if (_loginService == null)
        {
            _shopRefreshTimer = long.MaxValue;
            Debug.LogError("Login Service dependency not set for UnitShopService");
            return;
        }

        if (_loginService.UserContext == null)
        {
            Debug.LogWarning("User context is not set cannot do without user context");
        }

        _apiAdapter.GetAvailableUnits(_loginService.UserContext, OnGetSuccess, OnError);
        _shopRefreshTimer = shopRefreshInterval;
    }

    private void OnError(object sender, ErrorResponseDto e)
    {
        Debug.Log(e.message);
    }

    private void OnGetSuccess(object sender, UnitListResponseDto e)
    {
        AvailableUnits.Clear();
        if (e.units != null && e.units.Any())
        {
            AvailableUnits.AddRange(e.units.Select(x => x.ToUnitForSale()));
        }
    }

    public void BuyUnit(Unit unit)
    {
        _apiAdapter.BuyUnit(unit, _loginService.UserContext, OnBuyUnitSuccess, OnError);
    }

    private void OnBuyUnitSuccess(object sender, EmptyResponseDto e)
    {
        RefreshShop();
    }
}