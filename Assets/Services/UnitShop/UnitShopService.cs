using System.Collections.ObjectModel;
using System.Linq;
using Services.Dto;
using Services.UnitShop;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(GameService))]
[RequireComponent(typeof(LoginService))]
public class UnitShopService : MonoBehaviour
{
    [SerializeField] public float shopRefreshInterval = 15;
    private LoginService _loginService;
    private TavernServiceApiAdapter _apiAdapter;
    private float _shopRefreshTimer;
    private readonly ObservableCollection<UnitForSale> _availableUnits = new();

    public virtual ObservableCollection<UnitForSale> AvailableUnits => _availableUnits;

    private void Start()
    {
        _loginService = FindObjectOfType<LoginService>();
        _apiAdapter = gameObject.AddComponent<TavernServiceApiAdapter>();
        _apiAdapter.endpointAddress = FindObjectOfType<GameService>().endpointAddress;
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

    private void OnError(object sender, ErrorResponse e)
    {
        Debug.Log(e.message);
    }

    private void OnGetSuccess(object sender, GetAvailableUnitsResponse e)
    {
        _availableUnits.Clear();
        if (e.units != null && e.units.Any())
        {
            _availableUnits.AddRange(e.units.Select(x => x.ToUnitForSale()));
        }
    }

    public virtual void BuyUnit(Unit unit)
    {
        _apiAdapter.BuyUnit(unit, _loginService.UserContext, OnBuyUnitSuccess, OnError);
    }

    private void OnBuyUnitSuccess(object sender, EmptyResponse e)
    {
        RefreshShop();
    }
}