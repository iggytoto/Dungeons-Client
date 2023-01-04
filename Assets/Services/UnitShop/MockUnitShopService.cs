using System.Collections.ObjectModel;
using System.Linq;
using Services;

public class MockUnitShopService : ITavernService
{
    public ObservableCollection<UnitForSale> AvailableUnits { get; } = new()
    {
        UnitForSale.Of(Unit.HumanArcher(), 100),
        UnitForSale.Of(Unit.HumanWarrior(), 100)
    };

    public void BuyUnit(Unit unit)
    {
    }

    public string EndpointHttpType { get; set; }
    public string EndpointAddress { get; set; }
    public ushort EndpointPort { get; set; }

    public void InitService()
    {
    }
}