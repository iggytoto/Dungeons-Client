using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        _apiAdapter.TrainUnit(selectedUnitId, _loginService.UserContext, OnTrainSuccess, OnError);
    }

    private void Start()
    {
        _loginService = FindObjectOfType<GameService>().LoginService;
        _apiAdapter = gameObject.AddComponent<BarrackServiceApiAdapter>();
        _apiAdapter.endpointHttp = EndpointHttp;
        _apiAdapter.endpointAddress = EndpointHost;
        _apiAdapter.endpointPort = EndpointPrt;
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
        RefreshLocal(e?.units.Select(x => x.ToUnit()));
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
}