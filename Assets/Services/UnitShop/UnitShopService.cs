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
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                GetAvailableUnitsPath,
                null,
                ApiAdapter.Get,
                (_, dto) => OnGetSuccess(this, dto.items.Select(uDto => uDto.ToUnitForSale())),
                (_, dto) => OnError(this, dto.message),
                new DefaultDtoDeserializer<ListResponseDto<UnitDto>>()));
        _shopRefreshTimer = shopRefreshInterval;
    }

    private void OnError(object sender, string e)
    {
        Debug.Log(e);
    }

    private void OnGetSuccess(object sender, IEnumerable<UnitForSale> e)
    {
        AvailableUnits.Clear();
        AvailableUnits.AddRange(e);
    }

    public void BuyUnit(UnitType type)
    {
        StartCoroutine(
            APIAdapter.DoRequestCoroutine(
                BuyUnitPath,
                ApiAdapter.SerializeDto(new BuyUnitRequestDto { type = type }),
                ApiAdapter.Post,
                null,
                (_, dto) => OnError(this, dto.message),
                new UnitDtoDeserializer()));
    }
}