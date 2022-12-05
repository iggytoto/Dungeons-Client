using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MockBarracksService))]
public class MockUnitShopService : UnitShopService
{
    public override ObservableCollection<UnitForSale> AvailableUnits => _availableUnits;

    private readonly ObservableCollection<UnitForSale> _availableUnits = new()
    {
        UnitForSale.Of(Unit.Random(), 100),
        UnitForSale.Of(Unit.Random(), 100),
        UnitForSale.Of(Unit.Random(), 100),
        UnitForSale.Of(Unit.Random(), 100),
        UnitForSale.Of(Unit.Random(), 100),
    };

    private MockBarracksService _mockBarracksService;

    private void Start()
    {
        _mockBarracksService = FindObjectOfType<MockBarracksService>();
    }

    public override void BuyUnit(Unit unit)
    {
        var unitToBuy = _availableUnits.First(u => u.Id == unit.Id);
        AvailableUnits.Remove(unitToBuy);
        _mockBarracksService.AvailableUnits.Add(unitToBuy);
    }
}