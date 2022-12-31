using System.Collections.ObjectModel;
using System.Linq;
using Model.Units;
using Services;

public class MockBarracksService : IBarrackService
{
    public ObservableCollection<Unit> AvailableUnits { get; } = new()
    {
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
    };


    public void TrainUnit(long selectedUnitId)
    {
        AvailableUnits.Remove(AvailableUnits.First(u => u.Id == selectedUnitId));
    }

    public void ChangeUnitName(long selectedUnitId, string newName)
    {
        var u = AvailableUnits.First(u => u.Id == selectedUnitId);
        u.Name = newName;
    }

    public void ChangeUnitBattleBehavior(long selectedUnitId, BattleBehavior bb)
    {
        var u = AvailableUnits.First(u => u.Id == selectedUnitId);
        u.battleBehavior = bb;
    }

    public void UpgradeUnitEquipment(long equipmentId, UnitType unitType, string upgradeParamName)
    {
    }

    public string EndpointHttpType { get; set; }
    public string EndpointAddress { get; set; }
    public ushort EndpointPort { get; set; }

    public void InitService()
    {
    }
}