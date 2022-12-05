using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class UnitForSale : Unit
{
    public long GoldPrice { get; protected set; }

    public static UnitForSale Of(Unit unit, long goldPrice)
    {
        return new UnitForSale
        {
            Id = unit.Id,
            Armor = unit.Armor,
            Damage = unit.Damage,
            Icon = unit.Icon,
            Name = unit.Name,
            HitPoints = unit.HitPoints,
            MagicResistance = unit.MagicResistance,
            OwnerId = unit.OwnerId,
            TrainingExperience = unit.TrainingExperience,
            GoldPrice = goldPrice
        };
    }
}