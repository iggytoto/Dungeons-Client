using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Services.Barracks;
using Services.Dto;
using Services.UnitShop;
using Unity.VisualScripting;
using UnityEngine;

public class BarracksService : MonoBehaviour
{
    public virtual ObservableCollection<Unit> AvailableUnits => _availableUnits;
    private readonly ObservableCollection<Unit> _availableUnits = new();


    [SerializeField] public float refreshInterval = 15;
    private LoginService _loginService;
    private BarrackServiceApiAdapter _apiAdapter;
    private float _refreshTimer;

    public virtual void TrainUnit(long selectedUnitId)
    {
        _apiAdapter.TrainUnit(selectedUnitId, _loginService.UserContext, OnTrainSuccess, OnError);
    }

    private void Start()
    {
        _loginService = FindObjectOfType<LoginService>();
        _apiAdapter = gameObject.AddComponent<BarrackServiceApiAdapter>();
        _apiAdapter.endpointAddress = FindObjectOfType<GameService>().endpointAddress;
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

    private void OnGetSuccess(object sender, GetAvailableUnitsResponse e)
    {
        RefreshLocal(e?.units.Select(x => x.ToUnit()));
    }

    private void RefreshLocal(IEnumerable<Unit> e)
    {
        _availableUnits.Clear();
        if (e != null)
        {
            _availableUnits.AddRange(e);
        }
    }

    private static void OnError(object sender, ErrorResponse e)
    {
        Debug.Log(e.message);
    }

    private void OnTrainSuccess(object sender, TrainUnitResponse e)
    {
        Refresh();
    }
}