using System.Collections.ObjectModel;
using System.Linq;
using Services;

public class MockUnitShopService : ITavernService
{
    public ObservableCollection<UnitForSale> AvailableUnits { get; } = new()
    {
        UnitForSale.Of(Unit.Random(), 100),
        UnitForSale.Of(Unit.Random(), 100),
        UnitForSale.Of(Unit.Random(), 100),
        UnitForSale.Of(Unit.Random(), 100),
        UnitForSale.Of(Unit.Random(), 100),
    };

    public void BuyUnit(Unit unit)
    {
        var unitToBuy = AvailableUnits.First(u => u.Id == unit.Id);
        AvailableUnits.Remove(unitToBuy);
    }

    public string EndpointHttpType { get; set; }
    public string EndpointAddress { get; set; }
    public ushort EndpointPort { get; set; }

    public void InitService()
    {
    }
}