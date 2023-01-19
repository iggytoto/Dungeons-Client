using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Units;
using Services;
using Services.Common.Dto;
using Services.Dto;
using Services.UnitShop;
using Unity.VisualScripting;
using UnityEngine;

public class UnitShopService : ServiceBase, ITavernService
{
    private const string GetAvailableUnitsPath = "/tavern/availableUnits";
    private const string BuyUnitPath = "/tavern/buyUnit";

    [SerializeField] public float shopRefreshInterval = 15;

    private float _shopRefreshTimer;
    private ObservableCollection<Unit> _availableUnits;
    private ILoginService _loginService;

    public ObservableCollection<UnitForSale> AvailableUnits { get; } = new();

    private void Start()
    {
        _loginService = FindObjectOfType<GameService>().LoginService;
    }

    private void Update()
    {
        if (!(shopRefreshInterval > 0) || _loginService.UserContext == null) return;
        _shopRefreshTimer -= Time.deltaTime;
        if (_shopRefreshTimer <= 0)
        {
            RefreshShop(
                dto => OnGetSuccess(dto.items.Select(uDto => uDto.ToUnitForSale())),
                dto => OnError(dto.message));
        }
    }

    private void RefreshShop(Action<ListResponseDto<UnitDto>> onSuccess, Action<ErrorResponseDto> onError)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                GetAvailableUnitsPath,
                null,
                ApiAdapter.Get,
                onSuccess,
                onError,
                new DefaultListResponseDtoDeserializer<UnitDto>(new UnitDtoDeserializer())));
        _shopRefreshTimer = shopRefreshInterval;
    }

    private void OnError(string e)
    {
        Debug.Log(e);
    }

    private void OnGetSuccess(IEnumerable<UnitForSale> e)
    {
        AvailableUnits.Clear();
        AvailableUnits.AddRange(e);
    }

    public void BuyUnit(UnitType type, Action<Unit> onSuccess, Action<string> onError)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                BuyUnitPath,
                new BuyUnitRequestDto { type = type },
                ApiAdapter.Post,
                dto => onSuccess?.Invoke(dto.ToDomain()),
                dto => onError?.Invoke(dto.message),
                new UnitDtoDeserializer()));
    }
}