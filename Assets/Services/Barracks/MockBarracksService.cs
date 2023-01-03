using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Units;
using Services;
using Services.Common.Dto;

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

    public void UpgradeUnitEquipment<TDomain, TDto>(
        long equipmentId,
        UnitType unitType,
        string upgradeParamName,
        EventHandler<TDomain> onSuccess,
        Func<TDto, TDomain> dtoMapper)
        where TDomain : Equipment where TDto : EquipmentDto
    {
    }

    public string EndpointHttpType { get; set; }
    public string EndpointAddress { get; set; }
    public ushort EndpointPort { get; set; }

    public void InitService()
    {
    }
}