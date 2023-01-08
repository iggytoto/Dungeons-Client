using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Units;
using Model.Units.Humans;
using Services;
using UnityEngine;
using Random = System.Random;

public class MockUnitShopService : MonoBehaviour, ITavernService
{
    private MockBarracksService _mockBarrackService;

    private void Start()
    {
        _mockBarrackService = FindObjectOfType<MockBarracksService>();
    }

    public ObservableCollection<UnitForSale> AvailableUnits { get; } = new()
    {
        UnitForSale.Of(new HumanArcher(), 100),
        UnitForSale.Of(new HumanWarrior(), 100)
    };

    public void BuyUnit(UnitType type)
    {
        var rng = new Random();
        Unit u;
        switch (type)
        {
            case UnitType.HumanWarrior:
                u = new HumanWarrior() { Id = rng.Next(100000) };
                break;
            case UnitType.HumanArcher:
                u = new HumanArcher() { Id = rng.Next(100000) };
                break;
            case UnitType.HumanSpearman:
                u = new HumanSpearman() { Id = rng.Next(100000) };
                break;
            case UnitType.HumanCleric:
                u = new HumanCleric() { Id = rng.Next(100000) };
                break;
            case UnitType.Dummy:
            default:
                throw new InvalidOperationException();
        }

        _mockBarrackService.AvailableUnits.Add(u);
    }

    public string EndpointHttpType { get; set; }
    public string EndpointAddress { get; set; }
    public ushort EndpointPort { get; set; }

    public void InitService()
    {
    }
}