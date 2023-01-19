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

    public ObservableCollection<UnitForSale> AvailableUnits { get; } = new();


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
        if (!Initialized) return;
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                GetAvailableUnitsPath,
                null,
                ApiAdapter.Get,
                (_, dto) => OnGetSuccess(dto.items.Select(uDto => uDto.ToUnitForSale())),
                (_, dto) => OnError(dto.message),
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

    public void BuyUnit(UnitType type)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                BuyUnitPath,
                new BuyUnitRequestDto { type = type },
                ApiAdapter.Post,
                null,
                (_, dto) => OnError(dto.message),
                new UnitDtoDeserializer()));
    }
}