using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Items;
using Model.Units;
using Model.Units.Humans;
using Services;
using Services.Common.Dto;
using UnityEngine;

public class MockBarracksService : MonoBehaviour, IBarrackService
{
    public ObservableCollection<Unit> AvailableUnits { get; } = new()
    {
        new HumanWarrior() { Id = 1 },
        new HumanArcher() { Id = 2 },
        new HumanCleric() { Id = 3 },
        new HumanSpearman() { Id = 4 }
    };

    public ObservableCollection<Item> AvailableItems { get; } = new()
    {
        new Item { Id = 1 },
        new Item { Id = 2 },
        new Item { Id = 3 }
    };


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

    public void EquipItem(Item item, Unit unit)
    {
    }

    public void UnEquipItem(Item item)
    {
    }

    public void UpgradeUnitSkill<TDomain, TDto>(
        long skillId,
        UnitType unitType,
        string upgradeParamName,
        EventHandler<TDomain> onSuccess,
        Func<TDto, TDomain> dtoMapper)
        where TDomain : Skills where TDto : SkillsDto
    {
    }

    public string EndpointHttpType { get; set; }
    public string EndpointAddress { get; set; }
    public ushort EndpointPort { get; set; }

    public void InitService()
    {
    }
}